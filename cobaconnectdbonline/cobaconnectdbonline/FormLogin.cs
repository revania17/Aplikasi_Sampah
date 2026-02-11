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
        public Form1()
        {
            InitializeComponent();
            // All visual/design properties removed so you can edit them from the Designer/Properties.
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

        private void linkDaftar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // open registration form when user clicks "Daftar di sini"
            new FormRegis().Show();
            this.Hide();
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

        private void label4_Click(object sender, EventArgs e)
        {
        }
    }
}
