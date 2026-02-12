using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Drawing.Drawing2D;


namespace cobaconnectdbonline
{
    public partial class FormChatbot : Form
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private List<ChatMessage> _conversation = new List<ChatMessage>();

        private const string MistralApiUrl = "https://api.mistral.ai/v1/chat/completions";
        private readonly string MistralApiKey;
        private SupabaseService _supabase = new SupabaseService();


        public FormChatbot()
        {
            InitializeComponent();

            MistralApiKey = ConfigurationManager.AppSettings["MISTRAL_API_KEY"];

            this.Load += FormChatbot_Load;
            btnSend.Click += btnSend_Click;
            btnClear.Click += btnClear_Click;
            txtMessage.KeyPress += txtMessage_KeyPress;
        }

        private async void FormChatbot_Load(object sender, EventArgs e)
        {
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
            Panel row = new Panel
            {
                Width = chatPanel.ClientSize.Width - 30, 
                Height = 0, 
                AutoSize = true,
                Padding = new Padding(10),
                Margin = new Padding(0, 5, 0, 5),
                Dock = DockStyle.Bottom
            };

            Panel bubble = CreateRoundedBubble(text, isUser);

            if (isUser)
            {
                bubble.Left = row.Width - bubble.Width - 20; 
                bubble.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            }
            else
            {
                bubble.Left = 20; 
                bubble.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }

            row.Controls.Add(bubble);

            chatPanel.Controls.Add(row);

            chatPanel.VerticalScroll.Value = chatPanel.VerticalScroll.Maximum;
            chatPanel.ScrollControlIntoView(row);
        }

        private Panel CreateRoundedBubble(string text, bool isUser)
        {
            Color userColor = Color.CornflowerBlue;
            Color botColor = Color.FromArgb(235, 235, 240);

            Color borderColor = isUser ? Color.RoyalBlue : Color.DarkGray;

            Panel bubble = new Panel
            {
                AutoSize = true,
                Padding = new Padding(12),
                BackColor = isUser ? userColor : botColor,
                MaximumSize = new Size(400, 0),
            };

            bubble.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                int radius = 20;
                GraphicsPath path = new GraphicsPath();

                Rectangle r = bubble.ClientRectangle;
                r.Inflate(-1, -1);

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


            Label lbl = new Label
            {
                Text = text,
                AutoSize = true,
                MaximumSize = new Size(370, 0),
                ForeColor = isUser ? Color.White : Color.Black,
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular)
            };

            bubble.Controls.Add(lbl);
            return bubble;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            chatPanel.Controls.Clear();
            _conversation.Clear();

            _conversation.Add(new ChatMessage
            {
                role = "system",
                content =
                "Anda adalah chatbot edukasi pengelolaan sampah. " +
                "Anda HANYA boleh menjawab pertanyaan yang berkaitan dengan sampah, " +
                "seperti sampah organik, anorganik, daur ulang, pengelolaan sampah, dan kebersihan lingkungan. " +
                "Jika pengguna bertanya di luar topik sampah, tolak dengan sopan dan jelaskan bahwa chatbot ini hanya melayani edukasi tentang sampah."
            });


            lblStatus.Text = "Chat dibersihkan";
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

        public class ChatMessage
        {
            public string role { get; set; }
            public string content { get; set; }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new FormAdmin().Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
