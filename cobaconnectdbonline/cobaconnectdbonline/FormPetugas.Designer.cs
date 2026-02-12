namespace cobaconnectdbonline
{
    partial class FormPetugas
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
            this.pnlblue = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnDataSampah = new System.Windows.Forms.Button();
            this.btnKabko = new System.Windows.Forms.Button();
            this.btnDataPenjemputan = new System.Windows.Forms.Button();
            this.btnChatBot = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlblue.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlblue
            // 
            this.pnlblue.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlblue.Controls.Add(this.btnLogout);
            this.pnlblue.Controls.Add(this.btnDataSampah);
            this.pnlblue.Controls.Add(this.btnKabko);
            this.pnlblue.Controls.Add(this.btnDataPenjemputan);
            this.pnlblue.Controls.Add(this.btnChatBot);
            this.pnlblue.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlblue.Location = new System.Drawing.Point(0, 0);
            this.pnlblue.Name = "pnlblue";
            this.pnlblue.Size = new System.Drawing.Size(313, 712);
            this.pnlblue.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLogout.Location = new System.Drawing.Point(0, 667);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(313, 45);
            this.btnLogout.TabIndex = 13;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnDataSampah
            // 
            this.btnDataSampah.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDataSampah.FlatAppearance.BorderSize = 0;
            this.btnDataSampah.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnDataSampah.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataSampah.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataSampah.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDataSampah.Location = new System.Drawing.Point(0, 120);
            this.btnDataSampah.Name = "btnDataSampah";
            this.btnDataSampah.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnDataSampah.Size = new System.Drawing.Size(313, 60);
            this.btnDataSampah.TabIndex = 10;
            this.btnDataSampah.Text = "Data Sampah";
            this.btnDataSampah.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnDataSampah.UseVisualStyleBackColor = true;
            this.btnDataSampah.Click += new System.EventHandler(this.btnDataSampah_Click);
            // 
            // btnKabko
            // 
            this.btnKabko.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnKabko.FlatAppearance.BorderSize = 0;
            this.btnKabko.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnKabko.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKabko.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKabko.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnKabko.Location = new System.Drawing.Point(0, 60);
            this.btnKabko.Name = "btnKabko";
            this.btnKabko.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnKabko.Size = new System.Drawing.Size(313, 60);
            this.btnKabko.TabIndex = 8;
            this.btnKabko.Text = "Data Kabupaten Kota";
            this.btnKabko.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnKabko.UseVisualStyleBackColor = true;
            this.btnKabko.Click += new System.EventHandler(this.btnKabko_Click);
            // 
            // btnDataPenjemputan
            // 
            this.btnDataPenjemputan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDataPenjemputan.FlatAppearance.BorderSize = 0;
            this.btnDataPenjemputan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnDataPenjemputan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDataPenjemputan.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDataPenjemputan.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDataPenjemputan.Location = new System.Drawing.Point(0, 0);
            this.btnDataPenjemputan.Name = "btnDataPenjemputan";
            this.btnDataPenjemputan.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnDataPenjemputan.Size = new System.Drawing.Size(313, 60);
            this.btnDataPenjemputan.TabIndex = 12;
            this.btnDataPenjemputan.Text = "Data Penjemputan";
            this.btnDataPenjemputan.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnDataPenjemputan.UseVisualStyleBackColor = true;
            this.btnDataPenjemputan.Click += new System.EventHandler(this.btnDataPenjemputan_Click);
            // 
            // btnChatBot
            // 
            this.btnChatBot.FlatAppearance.BorderSize = 0;
            this.btnChatBot.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btnChatBot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChatBot.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChatBot.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnChatBot.Location = new System.Drawing.Point(0, 180);
            this.btnChatBot.Name = "btnChatBot";
            this.btnChatBot.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnChatBot.Size = new System.Drawing.Size(313, 60);
            this.btnChatBot.TabIndex = 11;
            this.btnChatBot.Text = "Chat Bot";
            this.btnChatBot.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnChatBot.UseVisualStyleBackColor = true;
            this.btnChatBot.Click += new System.EventHandler(this.btnChatBot_Click);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelMain.Controls.Add(this.label1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(313, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(987, 712);
            this.panelMain.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(720, 56);
            this.label1.TabIndex = 0;
            this.label1.Text = "Halo Selamat Datang Kembali!";
            // 
            // FormPetugas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 712);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.pnlblue);
            this.Name = "FormPetugas";
            this.Text = "Dashboard Petugas";
            this.Load += new System.EventHandler(this.FormPetugas_Load);
            this.pnlblue.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlblue;
        private System.Windows.Forms.Button btnDataSampah;
        private System.Windows.Forms.Button btnKabko;
        private System.Windows.Forms.Button btnDataPenjemputan;
        private System.Windows.Forms.Button btnChatBot;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label label1;
    }
}