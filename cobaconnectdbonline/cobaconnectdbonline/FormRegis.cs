using cobaconnectdbonline.Models;
using MongoDB.Driver;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace cobaconnectdbonline
{
    public partial class FormRegis : Form
    {
        public FormRegis()
        {
            InitializeComponent();

            // keep start position consistent with login
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormRegis_Load(object sender, EventArgs e)
        {
            // ensure runtime scaling/size behavior matches your design if needed
            this.AutoScaleMode = AutoScaleMode.None;
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

                // default role when role selection removed
                string defaultRole = "Petugas";

                User user = new User
                {
                    nama = txtNama.Text,
                    email = txtEmail.Text,
                    password = BCrypt.Net.BCrypt.HashPassword(txtPassword.Text),
                    role = defaultRole,
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

        private void linkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // buka form login ketika link diklik
            new Form1().Show();
            this.Hide();
        }
    }
}
