namespace cobaconnectdbonline
{
    partial class FormAdmin
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
            this.btnKabko = new System.Windows.Forms.Button();
            this.btnUser = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnJenisSampah = new System.Windows.Forms.Button();
            this.btnDataSampah = new System.Windows.Forms.Button();
            this.btnChatBot = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnKabko
            // 
            this.btnKabko.Location = new System.Drawing.Point(145, 171);
            this.btnKabko.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnKabko.Name = "btnKabko";
            this.btnKabko.Size = new System.Drawing.Size(209, 52);
            this.btnKabko.TabIndex = 0;
            this.btnKabko.Text = "Data Kabupaten Kota";
            this.btnKabko.UseVisualStyleBackColor = true;
            this.btnKabko.Click += new System.EventHandler(this.btnKabko_Click);
            // 
            // btnUser
            // 
            this.btnUser.Location = new System.Drawing.Point(145, 52);
            this.btnUser.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(209, 52);
            this.btnUser.TabIndex = 1;
            this.btnUser.Text = "Data Users";
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(767, 52);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(84, 29);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnJenisSampah
            // 
            this.btnJenisSampah.Location = new System.Drawing.Point(145, 284);
            this.btnJenisSampah.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnJenisSampah.Name = "btnJenisSampah";
            this.btnJenisSampah.Size = new System.Drawing.Size(209, 52);
            this.btnJenisSampah.TabIndex = 3;
            this.btnJenisSampah.Text = "Data Jenis Sampah";
            this.btnJenisSampah.UseVisualStyleBackColor = true;
            this.btnJenisSampah.Click += new System.EventHandler(this.btnJenisSampah_Click);
            // 
            // btnDataSampah
            // 
            this.btnDataSampah.Location = new System.Drawing.Point(145, 410);
            this.btnDataSampah.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDataSampah.Name = "btnDataSampah";
            this.btnDataSampah.Size = new System.Drawing.Size(209, 52);
            this.btnDataSampah.TabIndex = 4;
            this.btnDataSampah.Text = "Data Sampah";
            this.btnDataSampah.UseVisualStyleBackColor = true;
            this.btnDataSampah.Click += new System.EventHandler(this.btnDataSampah_Click);
            // 
            // btnChatBot
            // 
            this.btnChatBot.Location = new System.Drawing.Point(460, 223);
            this.btnChatBot.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnChatBot.Name = "btnChatBot";
            this.btnChatBot.Size = new System.Drawing.Size(209, 52);
            this.btnChatBot.TabIndex = 5;
            this.btnChatBot.Text = "Chat Bot";
            this.btnChatBot.UseVisualStyleBackColor = true;
            this.btnChatBot.Click += new System.EventHandler(this.btnChatBot_Click);
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 562);
            this.Controls.Add(this.btnChatBot);
            this.Controls.Add(this.btnDataSampah);
            this.Controls.Add(this.btnJenisSampah);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnUser);
            this.Controls.Add(this.btnKabko);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormAdmin";
            this.Text = "FormAdmin";
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnKabko;
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnJenisSampah;
        private System.Windows.Forms.Button btnDataSampah;
        private System.Windows.Forms.Button btnChatBot;
    }
}