using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace cobaconnectdbonline
{
    public partial class FormChatbot : Form
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private List<ChatMessage> _conversation = new List<ChatMessage>();
        private string _currentUserId;

        private const string MistralApiUrl = "https://api.mistral.ai/v1/chat/completions";
        private readonly string MistralApiKey;
        private SupabaseService _supabase = new SupabaseService();
        private IMongoDatabase _database;


        public FormChatbot(string username)
        {
            InitializeComponent();
            _currentUserId = username;
            MistralApiKey = ConfigurationManager.AppSettings["MISTRAL_API_KEY"];

            try
            {
                string connString = ConfigurationManager.ConnectionStrings["MongoDbConnection"].ConnectionString;
                var client = new MongoClient(connString);
                _database = client.GetDatabase("db_sampah");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal koneksi ke MongoDB: " + ex.Message);
            }

            this.Load += FormChatbot_Load;
            btnSend.Click += btnSend_Click;
            btnClear.Click += btnClear_Click;
            txtMessage.KeyPress += txtMessage_KeyPress;
        }

        private async void FormChatbot_Load(object sender, EventArgs e)
        {
            chatPanel.AutoScroll = true;

            if (string.IsNullOrEmpty(MistralApiKey))
            {
                MessageBox.Show("API Key Mistral belum diisi di App.config");
                this.Close();
                return;
            }

            cmbModel.Items.Add("mistral-tiny");
            cmbModel.Items.Add("mistral-small");
            cmbModel.Items.Add("mistral-medium");
            cmbModel.SelectedIndex = 0;

            _conversation.Add(new ChatMessage
            {
                role = "system",
                content =
                "Anda adalah chatbot edukasi pengelolaan sampah. " +
                "Anda HANYA boleh menjawab pertanyaan yang berkaitan dengan sampah, " +
                "seperti sampah organik, anorganik, daur ulang, pengelolaan sampah, dan kebersihan lingkungan. " +
                "Jika pengguna bertanya di luar topik sampah, tolak dengan sopan dan jelaskan bahwa chatbot ini hanya melayani edukasi tentang sampah."
            });

            lblStatus.Text = $"Memuat riwayat chat {_currentUserId}...";
            try
            {
                var history = await GetChatHistory(_currentUserId);

                if (history != null && history.Count > 0)
                {
                    //foreach (var chat in history)
                    //{
                    //    // Masukkan ke list percakapan agar AI ingat chat sebelumnya
                    //    _conversation.Add(new ChatMessage { role = chat.Role, content = chat.Content });

                    //    // Tampilkan di UI Chat Panel
                    //    AddBubble(chat.Content, chat.Role == "user");
                    //}
                }
            }
            catch (Exception ex)
            {
                // Jika error (misal collection belum ada), abaikan saja
            }

            await LoadHistoryToSidePanel();
            lblStatus.Text = "Siap";
        }

        // Generate embedding SEMUA data
        private async Task GenerateEmbeddingsToSupabase()
        {
            var docs = await _supabase.GetAllDocuments();

            foreach (var doc in docs)
            {
                string id = doc.id;
                string isi = doc.isi;

                // Generate vector embedding dari isi teks
                float[] embedding = await GetEmbedding(isi);

                await _supabase.UpdateEmbedding(id, embedding);
            }

            MessageBox.Show("Embedding selesai!");
        }

        // Generate embedding hanya untuk yang NULL saja
        private async Task GenerateEmbeddingForNullOnly()
        {
            var docs = await _supabase.GetAllDocuments();

            foreach (var doc in docs)
            {
                // Skip jika embedding sudah ada
                if (doc.embedding != null && doc.embedding.ToString() != "")
                    continue;

                string id = doc.id;
                string isi = doc.isi;

                var embedding = await GetEmbedding(isi);

                if (embedding != null)
                {
                    await _supabase.UpdateEmbedding(id, embedding);
                }
            }

            MessageBox.Show("Embedding NULL selesai diproses!");
        }

        // Generate embedding dari teks menggunakan Mistral
        private async Task<float[]> GetEmbedding(string text)
        {
            var requestBody = new
            {
                model = "mistral-embed",
                input = text
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {MistralApiKey}");

            var response = await _httpClient.PostAsync(
                "https://api.mistral.ai/v1/embeddings",
                content
            );

            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show(responseJson);
                return null;
            }

            dynamic result = JsonConvert.DeserializeObject(responseJson);

            if (result?.data == null)
                return null;

            // Ambil array vector dari response
            return ((IEnumerable<dynamic>)result.data[0].embedding)
                   .Select(x => (float)x)
                   .ToArray();
        }

        // Ambil dokumen paling mirip dari Supabase (Vector Search)
        private async Task<string> RetrieveKnowledge(string userMessage)
        {
            float[] queryEmbedding = await GetEmbedding(userMessage);

            var result = await _supabase.MatchDocument(queryEmbedding);

            if (result.similarity > 0.7) // threshold
                return result.isi;

            return null;
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            await SendMessage();
        }

        private async void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                await SendMessage();
            }
        }

        // Proses kirim pesan user
        private async Task SendMessage()
        {
            string message = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(message)) return;

            AddBubble(message, true);
            txtMessage.Clear();

            await SaveChat(_currentUserId, "user", message);
            lblStatus.Text = "Mengetik...";

            try
            {
                EnsureSystemMessage();

                // Ambil context dari Supabase
                string context = await RetrieveKnowledge(message);

                string finalMessage = message;

                // Jika ada context relevan → gabungkan
                if (!string.IsNullOrEmpty(context))
                {
                    finalMessage =
                        "Gunakan informasi berikut sebagai sumber utama. " +
                        "Anda boleh merangkum dengan bahasa sendiri, tetapi jangan menambahkan informasi di luar konteks.\n\n" +
                        context +
                        "\n\nPertanyaan: " + message;
                }

                _conversation.Add(new ChatMessage
                {
                    role = "user",
                    content = finalMessage
                });


                string response = await GetResponseFromMistral();

                _conversation.Add(new ChatMessage
                {
                    role = "assistant",
                    content = response
                });

                AddBubble(response, false);
                await SaveChat(_currentUserId, "assistant", response);
                await LoadHistoryToSidePanel();
                lblStatus.Text = "Siap";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                lblStatus.Text = "Error";
            }
        }

        // Request jawaban ke Mistral Chat API
        private async Task<string> GetResponseFromMistral()
        {
            string model = cmbModel.SelectedItem?.ToString() ?? "mistral-tiny";

            var requestData = new
            {
                model = model,
                messages = _conversation,
                temperature = 0.7,
                max_tokens = 500
            };

            var json = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {MistralApiKey}");

            var response = await _httpClient.PostAsync(MistralApiUrl, content);
            var responseJson = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(responseJson);
            }

            dynamic result = JsonConvert.DeserializeObject(responseJson);
            return result.choices[0].message.content;
        }

        private void AddBubble(string text, bool isUser)
        {
            int maxBubbleWidth = 400;
            int spacing = 10;

            Panel bubble = CreateRoundedBubble(text, isUser);
            bubble.MaximumSize = new Size(maxBubbleWidth, 0);
            bubble.AutoSize = true;

            chatPanel.Controls.Add(bubble);
            bubble.BringToFront();

            bubble.PerformLayout();
            bubble.Refresh();

            int x;
            int y = 10;

            if (chatPanel.Controls.Count > 1)
            {
                Control last = chatPanel.Controls[chatPanel.Controls.Count - 2];
                y = last.Bottom + spacing;
            }

            if (isUser)
            {
                x = chatPanel.ClientSize.Width - bubble.PreferredSize.Width - 20;
            }
            else
            {
                x = 10;
            }

            bubble.Location = new Point(x, y);

            chatPanel.ScrollControlIntoView(bubble);
        }

        private Panel CreateRoundedBubble(string text, bool isUser)
        {
            Color userColor = Color.CornflowerBlue;
            Color botColor = Color.FromArgb(235, 235, 240);

            Color borderColor = isUser ? Color.RoyalBlue : Color.DarkGray;

            Panel bubble = new Panel
            {
                AutoSize = true,
                Padding = new Padding(15), 
                BackColor = isUser ? userColor : botColor,
                MaximumSize = new Size(400, 0),
            };

            Label lbl = new Label
            {
                Text = text,
                AutoSize = true,
                MaximumSize = new Size(360, 0), 
                ForeColor = isUser ? Color.White : Color.Black,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(12, 12) 
            };

            bubble.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 20;
                GraphicsPath path = new GraphicsPath();
                Rectangle r = bubble.ClientRectangle;

                r.Width -= 1;
                r.Height -= 1;

                path.AddArc(r.X, r.Y, radius, radius, 180, 90);
                path.AddArc(r.Right - radius, r.Y, radius, radius, 270, 90);
                path.AddArc(r.Right - radius, r.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(r.X, r.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();

                bubble.Region = new Region(path);

                using (Pen pen = new Pen(borderColor, 2f))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            };

            bubble.Controls.Add(lbl);
            return bubble;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            chatPanel.Controls.Clear();
            _conversation.Clear();

            _currentSessionId = Guid.NewGuid().ToString();

            _conversation.Add(new ChatMessage
            {
                role = "system",
                content = "Anda adalah chatbot edukasi pengelolaan sampah. " +
                          "Anda HANYA boleh menjawab pertanyaan yang berkaitan dengan sampah, " +
                          "seperti sampah organik, anorganik, daur ulang, pengelolaan sampah, dan kebersihan lingkungan. " +
                          "Jika pengguna bertanya di luar topik sampah, tolak dengan sopan dan jelaskan bahwa chatbot ini hanya melayani edukasi tentang sampah."
            });

            lblStatus.Text = "Percakapan baru dimulai";
        }

        private void EnsureSystemMessage()
        {
            if (!_conversation.Any(m => m.role == "system"))
            {
                _conversation.Insert(0, new ChatMessage
                {
                    role = "system",
                    content =
                    "Anda adalah chatbot edukasi pengelolaan sampah. " +
                    "Hanya jawab pertanyaan yang berkaitan dengan sampah. " +
                    "Jika di luar topik, tolak dengan sopan."
                });
            }
        }

        // Simpan satu pesan ke MongoDB
        public async Task SaveChat(string userId, string role, string content)
        {
            var collection = _database.GetCollection<ChatHistoryModel>("ChatHistory");
            var newChat = new ChatHistoryModel
            {
                SessionId = _currentSessionId,
                UserId = userId,
                Role = role,
                Content = content,
                Timestamp = DateTime.Now
            };
            await collection.InsertOneAsync(newChat);
        }

        // Ambil riwayat chat berdasarkan UserId
        public async Task<List<ChatHistoryModel>> GetChatHistory(string userId)
        {
            var collection = _database.GetCollection<ChatHistoryModel>("ChatHistory");
            // Urutkan berdasarkan waktu (lama ke baru)
            return await collection.Find(c => c.UserId == userId)
                                   .SortBy(c => c.Timestamp)
                                   .ToListAsync();
        }

        private async Task LoadHistoryToSidePanel()
        {
            try
            {
                var collection = _database.GetCollection<ChatHistoryModel>("ChatHistory");

                // Ambil SEMUA chat user ini
                var allMessages = await collection.Find(c => c.UserId == _currentUserId).ToListAsync();

                listHistory.Items.Clear();

                // LOGIKA KUNCI: Kelompokkan berdasarkan SessionId
                var sessionGroups = allMessages
                    .Where(m => !string.IsNullOrEmpty(m.SessionId)) // Pastikan ada SessionId-nya
                    .GroupBy(m => m.SessionId)
                    .OrderByDescending(g => g.Max(m => m.Timestamp)) // Yang paling baru chat-nya di atas
                    .ToList();

                foreach (var group in sessionGroups)
                {
                    // Ambil pesan PERTAMA dari user di sesi ini sebagai "Judul"
                    var firstUserMsg = group
                        .Where(m => m.Role == "user")
                        .OrderBy(m => m.Timestamp)
                        .FirstOrDefault();

                    if (firstUserMsg != null)
                    {
                        string title = firstUserMsg.Content.Length > 20
                            ? firstUserMsg.Content.Substring(0, 20) + "..."
                            : firstUserMsg.Content;

                        listHistory.Items.Add(new HistoryItem { Title = title, SessionId = group.Key });
                    }
                }
            }
            catch (Exception ex) { /* log error jika perlu */ }
        }

        private async void listHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listHistory.SelectedItem is HistoryItem selectedHistory)
            {
                // 1. Sinkronkan ID Sesi agar tidak membuat log baru saat kirim pesan
                _currentSessionId = selectedHistory.SessionId;

                // 2. Bersihkan layar
                chatPanel.Controls.Clear();
                _conversation.Clear();
                EnsureSystemMessage();

                // 3. Ambil chat yang HANYA milik sesi ini
                var allHistory = await GetChatHistory(_currentUserId);
                var sessionHistory = allHistory.Where(h => h.SessionId == _currentSessionId).ToList();

                foreach (var chat in sessionHistory)
                {
                    // Tambahkan ke UI dan Memori AI
                    AddBubble(chat.Content, chat.Role == "user");
                    _conversation.Add(new ChatMessage { role = chat.Role, content = chat.Content });
                }
            }
        }

        public class ChatMessage
        {
            public string role { get; set; }
            public string content { get; set; }
        }

        public class ChatHistoryModel
        {
            [MongoDB.Bson.Serialization.Attributes.BsonId]
            [MongoDB.Bson.Serialization.Attributes.BsonIgnoreIfDefault]
            public MongoDB.Bson.ObjectId Id { get; set; }

            public string SessionId { get; set; } 
            public string UserId { get; set; }
            public string Role { get; set; }
            public string Content { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private string _currentSessionId = Guid.NewGuid().ToString();

        private void btnBack_Click(object sender, EventArgs e)
        {
            new FormAdmin().Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public class HistoryItem
        {
            public string Title { get; set; }
            public string SessionId { get; set; }
            public override string ToString() => Title; 
        }
    }
}
