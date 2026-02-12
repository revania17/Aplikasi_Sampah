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

        private async void Form1_Load(object sender, EventArgs e)
        {
            Database db = new Database();
            await db.SeedAdmin();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var db = new Database();
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            var user = db.Users.Find(u => u.email == email).FirstOrDefault();

            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.password))
                {
                    MessageBox.Show($"Selamat datang, {user.nama}!");

                    if (user.role == "Admin")
                    {
                        FormAdmin fAdmin = new FormAdmin();
                        fAdmin.Show();
                    }
                    else if (user.role == "Petugas")
                    {
                        FormPetugas fPetugas = new FormPetugas();
                        fPetugas.Show();
                    }
                    else
                    {
                        MessageBox.Show("Role tidak dikenali!");
                        return;
                    }

                    this.Hide(); 
                }
                else
                {
                    MessageBox.Show("Password salah!");
                }
            }
            else
            {
                MessageBox.Show("Email tidak terdaftar!");
            }
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
