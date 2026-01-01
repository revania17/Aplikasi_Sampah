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
            this.SuspendLayout();
            // 
            // btnKabko
            // 
            this.btnKabko.Location = new System.Drawing.Point(129, 166);
            this.btnKabko.Name = "btnKabko";
            this.btnKabko.Size = new System.Drawing.Size(186, 42);
            this.btnKabko.TabIndex = 0;
            this.btnKabko.Text = "Data Kabupaten Kota";
            this.btnKabko.UseVisualStyleBackColor = true;
            this.btnKabko.Click += new System.EventHandler(this.btnKabko_Click);
            // 
            // btnUser
            // 
            this.btnUser.Location = new System.Drawing.Point(129, 73);
            this.btnUser.Name = "btnUser";
            this.btnUser.Size = new System.Drawing.Size(186, 42);
            this.btnUser.TabIndex = 1;
            this.btnUser.Text = "Data Users";
            this.btnUser.UseVisualStyleBackColor = true;
            this.btnUser.Click += new System.EventHandler(this.btnUser_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(682, 42);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnUser);
            this.Controls.Add(this.btnKabko);
            this.Name = "FormAdmin";
            this.Text = "FormAdmin";
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnKabko;
        private System.Windows.Forms.Button btnUser;
        private System.Windows.Forms.Button btnLogout;
    }
}