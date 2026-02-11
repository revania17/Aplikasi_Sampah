using System;
using System.Linq;
using System.Windows.Forms;
using cobaconnectdbonline.Models;
using MongoDB.Driver;

namespace cobaconnectdbonline
{
    public partial class FormJenisSampah : Form
    {
        string selectedId = null;

        public FormJenisSampah()
        {
            InitializeComponent();

            this.Load += FormJenisSampah_Load;
            btnTambah.Click += btnTambah_Click;
            btnEdit.Click += btnEdit_Click;
            btnBack.Click += btnBack_Click;
            btnRefresh.Click += btnRefresh_Click;
            dgvJenisSampah.CellContentClick += dgvJenisSampah_CellContentClick;
        }

        private void FormJenisSampah_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var db = new Database();
            var list = db.JenisSampah.Find(FilterDefinition<JenisSampah>.Empty).ToList();
            dgvJenisSampah.DataSource = list;

            if (dgvJenisSampah.Columns.Contains("id_jenis"))
                dgvJenisSampah.Columns["id_jenis"].Visible = false;

            if (!dgvJenisSampah.Columns.Contains("Edit"))
            {
                var editBtn = new DataGridViewButtonColumn { Name = "Edit", HeaderText = "Edit", Text = "✏️", UseColumnTextForButtonValue = true };
                dgvJenisSampah.Columns.Add(editBtn);
            }

            if (!dgvJenisSampah.Columns.Contains("Delete"))
            {
                var deleteBtn = new DataGridViewButtonColumn { Name = "Delete", HeaderText = "Hapus", Text = "❌", UseColumnTextForButtonValue = true };
                dgvJenisSampah.Columns.Add(deleteBtn);
            }

            cmbJenisSampah.Items.Clear();
            var defaultItems = new[] { "Organik", "Anorganik", "B3", "Residu", "Elektronik" };
            foreach (var d in defaultItems) if (!cmbJenisSampah.Items.Contains(d)) cmbJenisSampah.Items.Add(d);
            foreach (var j in list.Select(x => x.nama_jenis).Distinct()) if (!string.IsNullOrWhiteSpace(j) && !cmbJenisSampah.Items.Contains(j)) cmbJenisSampah.Items.Add(j);
        }

        private void ClearForm()
        {
            cmbJenisSampah.SelectedIndex = -1;
            cmbJenisSampah.Text = string.Empty;
            txtKeterangan.Clear();
            selectedId = null;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbJenisSampah.Text)) { MessageBox.Show("Nama jenis tidak boleh kosong"); return; }

            var db = new Database();
            var jenis = new JenisSampah
            {
                // do not set id_jenis — driver will create ObjectId for _id
                nama_jenis = cmbJenisSampah.Text,
                keterangan = txtKeterangan.Text
            };

            db.JenisSampah.InsertOne(jenis);
            MessageBox.Show("Data berhasil ditambahkan");
            LoadData();
            ClearForm();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedId)) { MessageBox.Show("Klik Edit pada baris yang ingin diubah terlebih dahulu"); return; }
            if (string.IsNullOrWhiteSpace(cmbJenisSampah.Text)) { MessageBox.Show("Nama jenis tidak boleh kosong"); return; }

            var db = new Database();
            var filter = Builders<JenisSampah>.Filter.Eq(x => x.id_jenis, selectedId);
            var update = Builders<JenisSampah>.Update.Set(x => x.nama_jenis, cmbJenisSampah.Text).Set(x => x.keterangan, txtKeterangan.Text);
            db.JenisSampah.UpdateOne(filter, update);

            MessageBox.Show("Data berhasil diperbarui");
            LoadData();
            ClearForm();
        }

        private void dgvJenisSampah_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var idCell = dgvJenisSampah.Rows[e.RowIndex].Cells["id_jenis"].Value;
            if (idCell == null) return;
            selectedId = idCell.ToString();

            if (dgvJenisSampah.Columns[e.ColumnIndex].Name == "Edit")
            {
                cmbJenisSampah.Text = dgvJenisSampah.Rows[e.RowIndex].Cells["nama_jenis"].Value?.ToString();
                txtKeterangan.Text = dgvJenisSampah.Rows[e.RowIndex].Cells["keterangan"].Value?.ToString();
            }

            if (dgvJenisSampah.Columns[e.ColumnIndex].Name == "Delete")
            {
                if (MessageBox.Show("Yakin hapus data?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var db = new Database();
                    db.JenisSampah.DeleteOne(x => x.id_jenis == selectedId);
                    LoadData();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e) { new FormAdmin().Show(); this.Hide(); }
        private void btnRefresh_Click(object sender, EventArgs e) { LoadData(); ClearForm(); }
    }
}
