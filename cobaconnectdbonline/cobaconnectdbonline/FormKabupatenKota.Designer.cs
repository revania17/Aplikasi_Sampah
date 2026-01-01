namespace cobaconnectdbonline
{
    partial class FormKabupatenKota
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
            this.txtNamaWilayah = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvWilayah = new System.Windows.Forms.DataGridView();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWilayah)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNamaWilayah
            // 
            this.txtNamaWilayah.Location = new System.Drawing.Point(160, 72);
            this.txtNamaWilayah.Name = "txtNamaWilayah";
            this.txtNamaWilayah.Size = new System.Drawing.Size(123, 22);
            this.txtNamaWilayah.TabIndex = 0;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(12, 314);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(75, 23);
            this.btnTambah.TabIndex = 1;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Nama Wilayah:";
            // 
            // dgvWilayah
            // 
            this.dgvWilayah.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWilayah.Location = new System.Drawing.Point(326, 78);
            this.dgvWilayah.Name = "dgvWilayah";
            this.dgvWilayah.RowHeadersWidth = 51;
            this.dgvWilayah.RowTemplate.Height = 24;
            this.dgvWilayah.Size = new System.Drawing.Size(463, 329);
            this.dgvWilayah.TabIndex = 3;
            this.dgvWilayah.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWilayah_CellContentClick);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(123, 314);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 12);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "Kembali";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(67, 371);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // FormKabupatenKota
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.dgvWilayah);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.txtNamaWilayah);
            this.Name = "FormKabupatenKota";
            this.Text = "FormKabupatenKota";
            this.Load += new System.EventHandler(this.FormKabupatenKota_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWilayah)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNamaWilayah;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvWilayah;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnRefresh;
    }
}