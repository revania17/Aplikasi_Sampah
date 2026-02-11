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
    public partial class FormKabupatenKota : Form
    {
        public FormKabupatenKota()
        {
            InitializeComponent();
        }
        string selectedId = "";

        private void LoadData()
        {
            Database db = new Database();
            dgvWilayah.DataSource = db.Kabupaten.Find(_ => true).ToList();

            dgvWilayah.Columns["id_wilayah"].Visible = false;

            // Hapus kolom lama kalau reload
            if (!dgvWilayah.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn editBtn = new DataGridViewButtonColumn();
                editBtn.Name = "Edit";
                editBtn.HeaderText = "Edit";
                editBtn.Text = "✏️";
                editBtn.UseColumnTextForButtonValue = true;
                dgvWilayah.Columns.Add(editBtn);
            }

            if (!dgvWilayah.Columns.Contains("Delete"))
            {
                DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
                deleteBtn.Name = "Delete";
                deleteBtn.HeaderText = "Hapus";
                deleteBtn.Text = "❌";
                deleteBtn.UseColumnTextForButtonValue = true;
                dgvWilayah.Columns.Add(deleteBtn);
            }
        }


        private void btnTambah_Click(object sender, EventArgs e)
        {
            Database db = new Database();

            KabupatenKota data = new KabupatenKota
            {
                nama_wilayah = txtNamaWilayah.Text,
                provinsi = "Jawa Barat"
            };

            db.Kabupaten.InsertOne(data);

            MessageBox.Show("Data berhasil ditambahkan");
            LoadData();
            txtNamaWilayah.Clear();
        }

        private void FormKabupatenKota_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedId))
            {
                MessageBox.Show("Klik Edit dulu");
                return;
            }

            Database db = new Database();

            var filter = Builders<KabupatenKota>.Filter
                .Eq(x => x.id_wilayah, selectedId);

            var update = Builders<KabupatenKota>.Update
                .Set(x => x.nama_wilayah, txtNamaWilayah.Text);

            db.Kabupaten.UpdateOne(filter, update);

            MessageBox.Show("Data berhasil diperbarui");
            LoadData();
            txtNamaWilayah.Clear();
            selectedId = "";
        }

        private void dgvWilayah_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string id = dgvWilayah.Rows[e.RowIndex]
                .Cells["id_wilayah"].Value.ToString();

            // EDIT
            if (dgvWilayah.Columns[e.ColumnIndex].Name == "Edit")
            {
                selectedId = id;

                txtNamaWilayah.Text = dgvWilayah.Rows[e.RowIndex]
                    .Cells["nama_wilayah"].Value.ToString();
            }

            // DELETE
            if (dgvWilayah.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Yakin hapus data?",
                    "Konfirmasi",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Database db = new Database();
                    db.Kabupaten.DeleteOne(x => x.id_wilayah == id);
                    LoadData();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new FormAdmin().Show();
            this.Hide();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            txtNamaWilayah.Clear();  
            selectedId = "";
        }
    }
}
