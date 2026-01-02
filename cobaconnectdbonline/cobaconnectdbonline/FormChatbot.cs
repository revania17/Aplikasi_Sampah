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

        public FormChatbot()
        {
            InitializeComponent();

            MistralApiKey = ConfigurationManager.AppSettings["MISTRAL_API_KEY"];

            this.Load += FormChatbot_Load;
            btnSend.Click += btnSend_Click;
            btnClear.Click += btnClear_Click;
            txtMessage.KeyPress += txtMessage_KeyPress;
        }

        private void FormChatbot_Load(object sender, EventArgs e)
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
                content = "Anda adalah chatbot pengelolaan sampah Jawa Barat. Jawab dengan bahasa Indonesia yang sopan dan jelas."
            });

            lblStatus.Text = "Siap";
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

        private async Task SendMessage()
        {
            string message = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(message)) return;

            AddBubble(message, true);
            txtMessage.Clear();

            _conversation.Add(new ChatMessage
            {
                role = "user",
                content = message
            });

            lblStatus.Text = "Mengetik...";

            try
            {
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

        private async Task<string> GetResponseFromMistral()
        {
            var requestData = new
            {
                model = cmbModel.SelectedItem.ToString(),
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
                throw new Exception("Gagal terhubung ke Mistral AI");

            dynamic result = JsonConvert.DeserializeObject(responseJson);
            return result.choices[0].message.content;
        }

        private void AddBubble(string text, bool isUser)
        {
            FlowLayoutPanel row = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = false,
                FlowDirection = isUser ? FlowDirection.RightToLeft : FlowDirection.LeftToRight,
                Dock = DockStyle.Top,
                Padding = new Padding(10),
                Margin = new Padding(0)
            };

            Panel bubble = CreateRoundedBubble(text, isUser);

            row.Controls.Add(bubble);
            chatPanel.Controls.Add(row);
            chatPanel.ScrollControlIntoView(row);
        }

        private Panel CreateRoundedBubble(string text, bool isUser)
        {
            Color userColor = ColorTranslator.FromHtml("#f6d02d"); // kuning
            Color botColor = ColorTranslator.FromHtml("#2e7040");  // hijau

            Panel bubble = new Panel
            {
                AutoSize = true,
                Padding = new Padding(12),
                BackColor = isUser ? userColor : botColor,
                MaximumSize = new Size(350, 0),
                Margin = new Padding(5)
            };

            bubble.Paint += (s, e) =>
            {
                int radius = 18;
                GraphicsPath path = new GraphicsPath();
                Rectangle r = bubble.ClientRectangle;

                path.AddArc(r.X, r.Y, radius, radius, 180, 90);
                path.AddArc(r.Right - radius, r.Y, radius, radius, 270, 90);
                path.AddArc(r.Right - radius, r.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(r.X, r.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();

                bubble.Region = new Region(path);
            };

            Label lbl = new Label
            {
                Text = text,
                AutoSize = true,
                MaximumSize = new Size(320, 0),
                ForeColor = isUser ? Color.Black : Color.White,
                BackColor = Color.Transparent
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
                content = "chatbot pengelolaan sampah Jawa Barat."
            });

            lblStatus.Text = "Chat dibersihkan";
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
