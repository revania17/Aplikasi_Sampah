using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;

namespace cobaconnectdbonline
{
    public partial class Form1 : Form
    {
        private Image originalBackground;
        private Bitmap scaledBackground;

        public Form1()
        {
            InitializeComponent();
            // ensure we respond to resize so background always fills form without empty space
            this.Resize += Form1_Resize;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImageLayout = ImageLayout.None;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            originalBackground = Properties.Resources.sampah3;
            if (originalBackground != null)
            {
                // We'll scale & crop (cover) the image to exactly fill the client area.
                this.BackgroundImageLayout = ImageLayout.None;
                UpdateBackground();
                // You can allow resizing; background will adapt. If you want fixed size, keep FixedSingle.
                // this.FormBorderStyle = FormBorderStyle.FixedSingle;
                // this.MaximizeBox = false;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Recreate the scaled background when the client size changes
            if (originalBackground != null && this.ClientSize.Width > 0 && this.ClientSize.Height > 0)
            {
                UpdateBackground();
            }
        }

        private void UpdateBackground()
        {
            // Dispose previous scaled bitmap to avoid memory leak
            if (scaledBackground != null)
            {
                var prev = scaledBackground;
                scaledBackground = null;
                this.BackgroundImage = null;
                prev.Dispose();
            }

            // Create new scaled & cropped bitmap that "covers" the client area
            scaledBackground = ScaleAndCropToFill(originalBackground, this.ClientSize);
            this.BackgroundImage = scaledBackground;
        }

        // Scale source image to fill target size, cropping excess while preserving aspect ratio.
        private Bitmap ScaleAndCropToFill(Image src, Size target)
        {
            if (target.Width <= 0 || target.Height <= 0)
                return new Bitmap(1, 1);

            float scale = Math.Max((float)target.Width / src.Width, (float)target.Height / src.Height);
            int scaledWidth = (int)Math.Ceiling(src.Width * scale);
            int scaledHeight = (int)Math.Ceiling(src.Height * scale);

            var bmp = new Bitmap(target.Width, target.Height);
            bmp.SetResolution(src.HorizontalResolution, src.VerticalResolution);

            using (var g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                // compute offsets to center-crop the scaled image
                int offsetX = (target.Width - scaledWidth) / 2;
                int offsetY = (target.Height - scaledHeight) / 2;

                var destRect = new Rectangle(offsetX, offsetY, scaledWidth, scaledHeight);
                g.DrawImage(src, destRect, new Rectangle(0, 0, src.Width, src.Height), GraphicsUnit.Pixel);
            }

            return bmp;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Database db = new Database();

            var user = db.Users.Find(u => u.email == txtEmail.Text).FirstOrDefault();

            if (user == null)
            {
                MessageBox.Show("Email tidak ditemukan");
                return;
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(txtPassword.Text, user.password);

            if (!isValid)
            {
                MessageBox.Show("Password salah");
                return;
            }

            MessageBox.Show("Login berhasil sebagai " + user.role);

            // Contoh role
            if (user.role == "Admin")
            {
                new FormAdmin().Show();
            }
            else
            {
                new FormPetugas().Show();
            }

            this.Hide();
        }

        private void btnRegis_Click(object sender, EventArgs e)
        {
            new FormRegis().Show();
            this.Hide();
        }

        //private void TestMongoConnection()
        //{
        //    try
        //    {
        //        string connectionString =
        //        "mongodb+srv://bilaismina:Smp12345@sampah-cluster.6w4au7b.mongodb.net/?appName=Sampah-cluster";

        //        var client = new MongoClient(connectionString);

        //        // Ping ke server
        //        var database = client.GetDatabase("admin");
        //        var command = new MongoDB.Bson.BsonDocument("ping", 1);
        //        database.RunCommand<MongoDB.Bson.BsonDocument>(command);

        //        MessageBox.Show("✅ Koneksi ke MongoDB BERHASIL");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("❌ Koneksi GAGAL:\n" + ex.Message);
        //    }
        //}

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // clean up any created bitmap
            if (scaledBackground != null)
            {
                scaledBackground.Dispose();
                scaledBackground = null;
            }
            base.OnFormClosing(e);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
