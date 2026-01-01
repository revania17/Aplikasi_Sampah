using cobaconnectdbonline.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cobaconnectdbonline
{
    public partial class FormRegis : Form
    {
        public FormRegis()
        {
            InitializeComponent();
        }

        private void btnRegis_Click(object sender, EventArgs e)
        {
            try
            {
                Database db = new Database();

                // cek email sudah ada atau belum
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

                // optional: clear form
                txtNama.Clear();
                txtEmail.Clear();
                txtPassword.Clear();
                cmbRole.SelectedIndex = -1;
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
    }
}
