using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cobaconnectdbonline.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font.Constants;
using iText.Kernel.Font;

namespace cobaconnectdbonline
{
    public partial class FormDataSampah : Form
    {
        string selectedId = null;

        public FormDataSampah()
        {
            InitializeComponent();

            this.Load += FormDataSampah_Load;
            btnTambah.Click += btnTambah_Click;
            btnEdit.Click += btnEdit_Click;
            btnHapus.Click += btnHapus_Click;
            btnRefresh.Click += btnRefresh_Click;
            dgvDataSampah.CellClick += dgvDataSampah_CellClick;
            ExportButton.Click += ExportButton_Click;       

        }

        private void FormDataSampah_Load(object sender, EventArgs e)
        {
            LoadWilayah();
            LoadJenis();
            LoadData();
        }

        private void LoadWilayah()
        {
            var db = new Database();
            var wilayah = db.Kabupaten.Find(_ => true).ToList();

            cmbWilayah.DataSource = wilayah;
            cmbWilayah.DisplayMember = "nama_wilayah";
            cmbWilayah.ValueMember = "id_wilayah";
            cmbWilayah.SelectedIndex = -1;
        }

        private void LoadJenis()
        {
            var db = new Database();
            var jenis = db.JenisSampah.Find(_ => true).ToList();

            cmbJenis.DataSource = jenis;
            cmbJenis.DisplayMember = "nama_jenis";
            cmbJenis.ValueMember = "id_jenis";
            cmbJenis.SelectedIndex = -1;
        }

        private void LoadData()
        {
            var db = new Database();

            var wilayah = db.Kabupaten.Find(_ => true).ToList();
            var jenis = db.JenisSampah.Find(_ => true).ToList();

            var data = db.DataSampah.Find(_ => true).ToList()
                .Select(x => new
                {
                    id_sampah = x.id_sampah,
                    Wilayah = wilayah
                        .FirstOrDefault(w => w.id_wilayah == x.id_wilayah)?.nama_wilayah,
                    Jenis = jenis
                        .FirstOrDefault(j => j.id_jenis == x.id_jenis)?.nama_jenis,
                    x.Jumlah,
                    x.Tanggal,
                    x.Petugas
                }).ToList();

            dgvDataSampah.DataSource = data;

            if (dgvDataSampah.Columns.Contains("id_sampah"))
                dgvDataSampah.Columns["id_sampah"].Visible = false;
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (cmbWilayah.SelectedIndex == -1 || cmbJenis.SelectedIndex == -1)
            {
                MessageBox.Show("Wilayah dan jenis sampah wajib dipilih");
                return;
            }

            if (!double.TryParse(txtJumlah.Text, out double jumlah))
            {
                MessageBox.Show("Jumlah harus berupa angka");
                return;
            }

            var data = new DataSampah
            {
                id_wilayah = cmbWilayah.SelectedValue.ToString(),
                id_jenis = cmbJenis.SelectedValue.ToString(),
                Jumlah = jumlah,
                Tanggal = dtTanggal.Value,
                Petugas = txtPetugas.Text,
                CreatedAt = DateTime.Now
            };

            var db = new Database();
            db.DataSampah.InsertOne(data);

            MessageBox.Show("Data sampah berhasil ditambahkan");
            LoadData();
            ClearForm();
        }

        private void dgvDataSampah_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            selectedId = dgvDataSampah.Rows[e.RowIndex]
                .Cells["id_sampah"].Value.ToString();

            cmbWilayah.Text = dgvDataSampah.Rows[e.RowIndex]
                .Cells["Wilayah"].Value.ToString();

            cmbJenis.Text = dgvDataSampah.Rows[e.RowIndex]
                .Cells["Jenis"].Value.ToString();

            txtJumlah.Text = dgvDataSampah.Rows[e.RowIndex]
                .Cells["Jumlah"].Value.ToString();

            dtTanggal.Value = Convert.ToDateTime(
                dgvDataSampah.Rows[e.RowIndex].Cells["Tanggal"].Value);

            txtPetugas.Text = dgvDataSampah.Rows[e.RowIndex]
                .Cells["Petugas"].Value.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selectedId == null)
            {
                MessageBox.Show("Pilih data yang akan diedit");
                return;
            }

            var db = new Database();

            var filter = Builders<DataSampah>
                .Filter.Eq(x => x.id_sampah, selectedId);

            var update = Builders<DataSampah>.Update
                .Set(x => x.id_wilayah, cmbWilayah.SelectedValue.ToString())
                .Set(x => x.id_jenis, cmbJenis.SelectedValue.ToString())
                .Set(x => x.Jumlah, Convert.ToDouble(txtJumlah.Text))
                .Set(x => x.Tanggal, dtTanggal.Value)
                .Set(x => x.Petugas, txtPetugas.Text);

            db.DataSampah.UpdateOne(filter, update);

            MessageBox.Show("Data berhasil diperbarui");
            LoadData();
            ClearForm();
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedId == null)
            {
                MessageBox.Show("Pilih data yang akan dihapus");
                return;
            }

            if (MessageBox.Show("Yakin hapus data?", "Konfirmasi",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var db = new Database();
                db.DataSampah.DeleteOne(x => x.id_sampah == selectedId);

                LoadData();
                ClearForm();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
            ClearForm();
        }

        private void ClearForm()
        {
            cmbWilayah.SelectedIndex = -1;
            cmbJenis.SelectedIndex = -1;
            txtJumlah.Clear();
            txtPetugas.Clear();
            selectedId = null;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            new FormAdmin().Show();
            this.Hide();
        }
        private void ExportButton_Click(object sender, EventArgs e)
        {
            if (dgvDataSampah.DataSource == null || dgvDataSampah.Rows.Count == 0)
            {
                MessageBox.Show("Tidak ada data untuk diekspor ke PDF.");
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                Title = "Simpan Laporan Data Sampah",
                FileName = "Laporan_Sampah_" + DateTime.Now.ToString("yyyyMMdd")
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (PdfWriter writer = new PdfWriter(saveFileDialog.FileName))
                    {
                        using (PdfDocument pdf = new PdfDocument(writer))
                        {
                            Document document = new Document(pdf);

                            // Menghitung jumlah kolom yang tampil (Visible) saja
                            int visibleColumnCount = 0;
                            foreach (DataGridViewColumn col in dgvDataSampah.Columns)
                            {
                                if (col.Name != "id_sampah") visibleColumnCount++;
                            }

                            Table table = new Table(visibleColumnCount);

                            // 1. Membuat Header (Hanya kolom yang bukan id_sampah)
                            foreach (DataGridViewColumn column in dgvDataSampah.Columns)
                            {
                                if (column.Name == "id_sampah") continue; // Lewati kolom ID

                                Cell headerCell = new Cell().Add(new iText.Layout.Element.Paragraph(column.HeaderText));
                                headerCell.SetBackgroundColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY);
                                table.AddHeaderCell(headerCell);
                            }

                            // 2. Mengambil Data (Hanya kolom yang bukan id_sampah)
                            foreach (DataGridViewRow row in dgvDataSampah.Rows)
                            {
                                if (row.IsNewRow) continue;

                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    // Cek berdasarkan nama kolom agar ID tidak terbawa
                                    if (dgvDataSampah.Columns[cell.ColumnIndex].Name == "id_sampah") continue;

                                    string cellValue = cell.Value?.ToString() ?? "";
                                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(cellValue)));
                                }
                            }

                            document.Add(new iText.Layout.Element.Paragraph("LAPORAN DATA SAMPAH").SetFontSize(16));
                            document.Add(table);
                            document.Close();
                        }
                    }
                    MessageBox.Show("PDF Berhasil dibuat tanpa kolom ID!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal ekspor PDF: " + ex.Message, "Error");
                }
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Perbaikan: Gunakan class Database() untuk mengambil koleksi
            var db = new Database();
            var documents = db.DataSampah.Find(new BsonDocument()).ToList();

            DataTable dataTable = new DataTable();

            if (documents.Count > 0)
            {
                // Kita ambil elemen dari dokumen pertama untuk jadi Header kolom
                // Catatan: Ini menggunakan data mentah BsonDocument
                foreach (var element in documents[0].ToBsonDocument().Elements)
                {
                    dataTable.Columns.Add(element.Name);
                }
            }

            foreach (var doc in documents)
            {
                DataRow row = dataTable.NewRow();
                var bsonDoc = doc.ToBsonDocument();
                foreach (var element in bsonDoc.Elements)
                {
                    row[element.Name] = element.Value.ToString();
                }
                dataTable.Rows.Add(row);
            }

            // Perbaikan: Ganti dataGridView1 menjadi dgvDataSampah
            dgvDataSampah.DataSource = dataTable;
        }
    }
}
