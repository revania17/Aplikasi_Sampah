using cobaconnectdbonline.Models;
using MongoDB.Driver;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace cobaconnectdbonline
{
    public partial class FormRegis : Form
    {
        private Image originalBackground;
        private Bitmap scaledBackground;

        public FormRegis()
        {
            InitializeComponent();

            // agar background menyesuaikan saat resize
            this.Resize += FormRegis_Resize;

            // optional tapi bagus
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormRegis_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.sampah;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void FormRegis_Resize(object sender, EventArgs e)
        {
            if (originalBackground != null &&
                this.ClientSize.Width > 0 &&
                this.ClientSize.Height > 0)
            {
                UpdateBackground();
            }
        }

        private void UpdateBackground()
        {
            if (scaledBackground != null)
            {
                var prev = scaledBackground;
                scaledBackground = null;
                this.BackgroundImage = null;
                prev.Dispose();
            }

            scaledBackground = ScaleAndCropToFill(originalBackground, this.ClientSize);
            this.BackgroundImage = scaledBackground;
        }

        private Bitmap ScaleAndCropToFill(Image src, Size target)
        {
            float scale = Math.Max(
                (float)target.Width / src.Width,
                (float)target.Height / src.Height
            );

            int scaledW = (int)Math.Ceiling(src.Width * scale);
            int scaledH = (int)Math.Ceiling(src.Height * scale);

            Bitmap bmp = new Bitmap(target.Width, target.Height);
            bmp.SetResolution(src.HorizontalResolution, src.VerticalResolution);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                int offsetX = (target.Width - scaledW) / 2;
                int offsetY = (target.Height - scaledH) / 2;

                g.DrawImage(
                    src,
                    new Rectangle(offsetX, offsetY, scaledW, scaledH),
                    new Rectangle(0, 0, src.Width, src.Height),
                    GraphicsUnit.Pixel
                );
            }

            return bmp;
        }

        private void btnRegis_Click(object sender, EventArgs e)
        {
            try
            {
                Database db = new Database();

                var existingUser = db.Users
                    .Find(u => u.email == txtEmail.Text)
                    .FirstOrDefault();

                if (existingUser != null)
                {
                    MessageBox.Show("Email sudah terdaftar");
                    return;
                }

                User user = new User
                {
                    nama = txtNama.Text,
                    email = txtEmail.Text,
                    password = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text),
                    role = cmbRole.Text,
                    created_at = DateTime.Now
                };

                db.Users.InsertOne(user);

                MessageBox.Show("Registrasi berhasil, silakan login");

                new Form1().Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Register:\n" + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (scaledBackground != null)
            {
                scaledBackground.Dispose();
                scaledBackground = null;
            }
            base.OnFormClosing(e);
        }
    }
}
