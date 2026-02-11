using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using cobaconnectdbonline.Models;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace cobaconnectdbonline
{
    public partial class DataPenjemputan : Form
    {
        private Database db;
        private GMapOverlay markerOverlay;

        // Simpan ID sebagai STRING (AMAN)
        private string selectedId = null;

        public DataPenjemputan()
        {
            InitializeComponent();

            db = new Database();

            InitMap();
            SetupGrid();
            LoadDataPenjemputan();

            dgvPenjemputan.CellClick += dgvPenjemputan_CellClick;
            btnSimpan.Click += BtnSimpan_Click;
            btnRefresh.Click += BtnRefresh_Click;
            gMapControl1.MouseClick += GMapControl1_MouseClick;

            SetAddMode();
        }

        // ================= MODE =================

        private void SetAddMode()
        {
            selectedId = null;
            btnSimpan.Text = "Simpan";
        }

        private void SetEditMode()
        {
            btnSimpan.Text = "Update";
        }

        // ================= MAP =================

        private void InitMap()
        {
            gMapControl1.MapProvider = GMapProviders.GoogleMap;

            gMapControl1.Position =
                new PointLatLng(-6.200000, 106.816666);

            gMapControl1.MinZoom = 2;
            gMapControl1.MaxZoom = 18;
            gMapControl1.Zoom = 12;

            gMapControl1.ShowCenter = false;

            markerOverlay = new GMapOverlay("markers");
            gMapControl1.Overlays.Add(markerOverlay);
        }

        // ================= GRID =================

        private void SetupGrid()
        {
            dgvPenjemputan.AutoGenerateColumns = false;
            dgvPenjemputan.Columns.Clear();

            dgvPenjemputan.Columns.Add("Id", "ID");
            dgvPenjemputan.Columns["Id"].Visible = false;

            dgvPenjemputan.Columns.Add("Nama", "Nama");
            dgvPenjemputan.Columns.Add("Status", "Status");
            dgvPenjemputan.Columns.Add("Latitude", "Latitude");
            dgvPenjemputan.Columns.Add("Longitude", "Longitude");
            dgvPenjemputan.Columns.Add("Tanggal", "Tanggal");
        }

        // ================= LOAD =================

        private void LoadDataPenjemputan()
        {
            try
            {
                dgvPenjemputan.Rows.Clear();

                var data = db.Data_Penjemputan
                    .Find(Builders<Data_Penjemputan>.Filter.Empty)
                    .ToList();

                foreach (var d in data)
                {
                    dgvPenjemputan.Rows.Add(
                        d.Id,
                        d.Nama,
                        d.Status,
                        d.Latitude,
                        d.Longitude,
                        d.Tanggal ?? DateTime.Now
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal load:\n" + ex.Message);
            }
        }

        // ================= GRID CLICK =================

        private void dgvPenjemputan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvPenjemputan.Rows[e.RowIndex];

            // Ambil ID
            selectedId = row.Cells["Id"].Value?.ToString();

            txtNama.Text = row.Cells["Nama"].Value?.ToString();
            cmbStatus.Text = row.Cells["Status"].Value?.ToString();
            txtLatitude.Text = row.Cells["Latitude"].Value?.ToString();
            txtLongitude.Text = row.Cells["Longitude"].Value?.ToString();

            // AMAN tanggal
            if (DateTime.TryParse(row.Cells["Tanggal"].Value?.ToString(), out DateTime tgl))
            {
                if (tgl >= dtpTanggal.MinDate && tgl <= dtpTanggal.MaxDate)
                    dtpTanggal.Value = tgl;
                else
                    dtpTanggal.Value = DateTime.Now;
            }
            else
            {
                dtpTanggal.Value = DateTime.Now;
            }

            SetEditMode();
        }

        // ================= MAP CLICK =================

        private void GMapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            var point =
                gMapControl1.FromLocalToLatLng(e.X, e.Y);

            txtLatitude.Text = point.Lat.ToString("F6");
            txtLongitude.Text = point.Lng.ToString("F6");

            markerOverlay.Markers.Clear();

            markerOverlay.Markers.Add(
                new GMarkerGoogle(point, GMarkerGoogleType.blue_dot)
            );

            gMapControl1.Refresh();
        }

        // ================= SIMPAN =================

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (!Validasi()) return;

            double lat = double.Parse(txtLatitude.Text);
            double lng = double.Parse(txtLongitude.Text);

            try
            {
                if (selectedId == null)
                    InsertData(lat, lng);
                else
                    UpdateData(lat, lng);

                ClearInput();
                LoadDataPenjemputan();
                SetAddMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message);
            }
        }

        // ================= INSERT =================

        private void InsertData(double lat, double lng)
        {
            var data = new Data_Penjemputan
            {
                Nama = txtNama.Text.Trim(),
                Latitude = lat,
                Longitude = lng,
                Status = cmbStatus.Text,
                Tanggal = dtpTanggal.Value,
                CreatedAt = DateTime.Now
            };

            db.Data_Penjemputan.InsertOne(data);

            MessageBox.Show("Data berhasil ditambahkan");
        }

        // ================= UPDATE =================

        private void UpdateData(double lat, double lng)
        {
            var filter = Builders<Data_Penjemputan>.Filter
                .Eq(x => x.Id, selectedId);

            var update = Builders<Data_Penjemputan>.Update
                .Set(x => x.Nama, txtNama.Text.Trim())
                .Set(x => x.Status, cmbStatus.Text)
                .Set(x => x.Latitude, lat)
                .Set(x => x.Longitude, lng)
                .Set(x => x.Tanggal, dtpTanggal.Value);

            db.Data_Penjemputan.UpdateOne(filter, update);

            MessageBox.Show("Data berhasil diupdate");
        }

        // ================= REFRESH =================

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            ClearInput();
            LoadDataPenjemputan();
            SetAddMode();
        }

        // ================= VALIDASI =================

        private bool Validasi()
        {
            if (string.IsNullOrWhiteSpace(txtNama.Text))
            {
                MessageBox.Show("Nama wajib diisi");
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbStatus.Text))
            {
                MessageBox.Show("Status wajib diisi");
                return false;
            }

            if (!double.TryParse(txtLatitude.Text, out _))
            {
                MessageBox.Show("Latitude tidak valid");
                return false;
            }

            if (!double.TryParse(txtLongitude.Text, out _))
            {
                MessageBox.Show("Longitude tidak valid");
                return false;
            }

            return true;
        }

        // ================= CLEAR =================

        private void ClearInput()
        {
            txtNama.Clear();
            txtLatitude.Clear();
            txtLongitude.Clear();

            cmbStatus.SelectedIndex = -1;
            dtpTanggal.Value = DateTime.Now;

            markerOverlay.Markers.Clear();
            gMapControl1.Refresh();

            selectedId = null;
        }
    }
}
