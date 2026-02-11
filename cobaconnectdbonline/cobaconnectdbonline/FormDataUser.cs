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
    public partial class FormDataUser : Form
    {
        public FormDataUser()
        {
            InitializeComponent();
        }
        string selectedId = "";

        private void LoadData()
        {
            Database db = new Database();
            dgvUsers.DataSource = db.Users.Find(_ => true).ToList();

            dgvUsers.Columns["id_user"].Visible = false;
            dgvUsers.Columns["password"].Visible = false;

            if (!dgvUsers.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn editBtn = new DataGridViewButtonColumn();
                editBtn.Name = "Edit";
                editBtn.HeaderText = "Edit";
                editBtn.Text = "✏️";
                editBtn.UseColumnTextForButtonValue = true;
                dgvUsers.Columns.Add(editBtn);
            }

            if (!dgvUsers.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
                deleteBtn.Name = "Delete";
                deleteBtn.HeaderText = "Hapus";
                deleteBtn.Text = "❌";
                deleteBtn.UseColumnTextForButtonValue = true;
                dgvUsers.Columns.Add(deleteBtn);
            }
        }
        private void btnTambah_Click(object sender, EventArgs e)
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

                MessageBox.Show("User berhasil ditambahkan");
                LoadData();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedId))
            {
                MessageBox.Show("Klik Edit dulu");
                return;
            }

            Database db = new Database();

            var filter = Builders<User>.Filter
                .Eq(x => x.id_user, selectedId);

            var update = Builders<User>.Update
                .Set(x => x.nama, txtNama.Text)
                .Set(x => x.email, txtEmail.Text)
                .Set(x => x.role, cmbRole.Text);

            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                update = update.Set(
                    x => x.password,
                    BCrypt.Net.BCrypt.HashPassword(txtPassword.Text)
                );
            }

            db.Users.UpdateOne(filter, update);

            MessageBox.Show("User berhasil diperbarui");
            LoadData();
            ClearForm();
            selectedId = "";
        }

        private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string id = dgvUsers.Rows[e.RowIndex]
                .Cells["id_user"].Value.ToString();

            // EDIT
            if (dgvUsers.Columns[e.ColumnIndex].Name == "Edit")
            {
                selectedId = id;

                txtNama.Text = dgvUsers.Rows[e.RowIndex]
                    .Cells["nama"].Value.ToString();

                txtEmail.Text = dgvUsers.Rows[e.RowIndex]
                    .Cells["email"].Value.ToString();

                cmbRole.Text = dgvUsers.Rows[e.RowIndex]
                    .Cells["role"].Value.ToString();

                txtPassword.Clear(); // kosongkan, tidak auto tampil
            }

            // DELETE
            if (dgvUsers.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Yakin hapus user?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Database db = new Database();
                    db.Users.DeleteOne(x => x.id_user == id);
                    LoadData();
                }
            }
        }
        private void ClearForm()
        {
            txtNama.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = -1;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new FormAdmin().Show();
            this.Hide();
        }

        private void FormDataUser_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();     
            ClearForm();     
            selectedId = "";
        }
    }
}
