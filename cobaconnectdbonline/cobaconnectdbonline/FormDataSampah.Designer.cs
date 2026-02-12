namespace cobaconnectdbonline
{
    partial class FormDataSampah
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.cmbWilayah = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJumlah = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtTanggal = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPetugas = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvDataSampah = new System.Windows.Forms.DataGridView();
            this.ExportButton = new System.Windows.Forms.Button();
            this.chartSampah = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txtJenis = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataSampah)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSampah)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbWilayah
            // 
            this.cmbWilayah.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWilayah.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.cmbWilayah.FormattingEnabled = true;
            this.cmbWilayah.Location = new System.Drawing.Point(226, 78);
            this.cmbWilayah.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbWilayah.Name = "cmbWilayah";
            this.cmbWilayah.Size = new System.Drawing.Size(198, 31);
            this.cmbWilayah.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(52, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Wilayah :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(52, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Jenis Sampah :";
            // 
            // txtJumlah
            // 
            this.txtJumlah.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJumlah.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtJumlah.Location = new System.Drawing.Point(226, 174);
            this.txtJumlah.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtJumlah.Name = "txtJumlah";
            this.txtJumlah.Size = new System.Drawing.Size(198, 32);
            this.txtJumlah.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(52, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Jumlah (kg) :";
            // 
            // dtTanggal
            // 
            this.dtTanggal.CalendarFont = new System.Drawing.Font("Century Gothic", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTanggal.CalendarForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dtTanggal.CalendarMonthBackground = System.Drawing.Color.Transparent;
            this.dtTanggal.CalendarTitleBackColor = System.Drawing.Color.Transparent;
            this.dtTanggal.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtTanggal.Location = new System.Drawing.Point(226, 224);
            this.dtTanggal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtTanggal.Name = "dtTanggal";
            this.dtTanggal.Size = new System.Drawing.Size(198, 26);
            this.dtTanggal.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Location = new System.Drawing.Point(52, 224);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tanggal :";
            // 
            // txtPetugas
            // 
            this.txtPetugas.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPetugas.Location = new System.Drawing.Point(226, 270);
            this.txtPetugas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPetugas.Name = "txtPetugas";
            this.txtPetugas.Size = new System.Drawing.Size(198, 32);
            this.txtPetugas.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(52, 270);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 23);
            this.label5.TabIndex = 9;
            this.label5.Text = "Petugas :";
            // 
            // btnTambah
            // 
            this.btnTambah.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnTambah.FlatAppearance.BorderSize = 0;
            this.btnTambah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTambah.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTambah.ForeColor = System.Drawing.Color.White;
            this.btnTambah.Location = new System.Drawing.Point(177, 340);
            this.btnTambah.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(115, 37);
            this.btnTambah.TabIndex = 10;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.ForeColor = System.Drawing.Color.White;
            this.btnEdit.Location = new System.Drawing.Point(298, 340);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(115, 37);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnHapus
            // 
            this.btnHapus.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnHapus.FlatAppearance.BorderSize = 0;
            this.btnHapus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHapus.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHapus.ForeColor = System.Drawing.Color.White;
            this.btnHapus.Location = new System.Drawing.Point(56, 340);
            this.btnHapus.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(115, 37);
            this.btnHapus.TabIndex = 12;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = false;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click_1);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(462, 7);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(115, 39);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // dgvDataSampah
            // 
            this.dgvDataSampah.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDataSampah.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDataSampah.BackgroundColor = System.Drawing.Color.White;
            this.dgvDataSampah.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataSampah.Location = new System.Drawing.Point(462, 50);
            this.dgvDataSampah.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDataSampah.Name = "dgvDataSampah";
            this.dgvDataSampah.ReadOnly = true;
            this.dgvDataSampah.RowHeadersWidth = 62;
            this.dgvDataSampah.RowTemplate.Height = 28;
            this.dgvDataSampah.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDataSampah.Size = new System.Drawing.Size(811, 327);
            this.dgvDataSampah.TabIndex = 14;
            // 
            // ExportButton
            // 
            this.ExportButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ExportButton.FlatAppearance.BorderSize = 0;
            this.ExportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExportButton.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportButton.ForeColor = System.Drawing.Color.White;
            this.ExportButton.Location = new System.Drawing.Point(1158, 7);
            this.ExportButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ExportButton.Name = "ExportButton";
            this.ExportButton.Size = new System.Drawing.Size(115, 39);
            this.ExportButton.TabIndex = 16;
            this.ExportButton.Text = "Export PDF";
            this.ExportButton.UseVisualStyleBackColor = false;
            // 
            // chartSampah
            // 
            this.chartSampah.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartSampah.BorderlineColor = System.Drawing.Color.LightGray;
            chartArea1.Name = "ChartArea1";
            this.chartSampah.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartSampah.Legends.Add(legend1);
            this.chartSampah.Location = new System.Drawing.Point(462, 396);
            this.chartSampah.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartSampah.Name = "chartSampah";
            this.chartSampah.Size = new System.Drawing.Size(811, 286);
            this.chartSampah.TabIndex = 17;
            this.chartSampah.Text = "chartSampah";
            // 
            // txtJenis
            // 
            this.txtJenis.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJenis.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtJenis.Location = new System.Drawing.Point(226, 121);
            this.txtJenis.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtJenis.Name = "txtJenis";
            this.txtJenis.Size = new System.Drawing.Size(198, 32);
            this.txtJenis.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(86, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(307, 27);
            this.label6.TabIndex = 19;
            this.label6.Text = "Manajemen Data Sampah";
            // 
            // FormDataSampah
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1300, 712);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtJenis);
            this.Controls.Add(this.chartSampah);
            this.Controls.Add(this.ExportButton);
            this.Controls.Add(this.dgvDataSampah);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPetugas);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtTanggal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtJumlah);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbWilayah);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormDataSampah";
            this.Text = "Manajemen Data Sampah";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataSampah)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartSampah)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbWilayah;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJumlah;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtTanggal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPetugas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvDataSampah;
        private System.Windows.Forms.Button ExportButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSampah;
        private System.Windows.Forms.TextBox txtJenis;
        private System.Windows.Forms.Label label6;
    }
}