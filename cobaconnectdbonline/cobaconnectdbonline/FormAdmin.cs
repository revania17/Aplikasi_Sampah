using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;

namespace cobaconnectdbonline
{
    public partial class FormAdmin : Form
    {

        public FormAdmin()
        {
            InitializeComponent();

            this.Size = new Size(1318, 759);


            this.AutoScaleMode = AutoScaleMode.None;
        }


        private void FormAdmin_Load(object sender, EventArgs e)
        {
           
        }
        private void LoadForm(Form frm, object btnSender)
        {
            ActivateButton(btnSender);

            panelMain.Controls.Clear();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelMain.Controls.Add(frm);
            frm.Show();
        }


        private void btnKabko_Click(object sender, EventArgs e)
        {
            LoadForm(new FormKabupatenKota(), sender);
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            LoadForm(new FormDataUser(), sender);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin ingin logout?",
        "Konfirmasi",
        MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Form1 login = new Form1();
                login.Show();
                this.Hide();
            }
        }

        private void btnJenisSampah_Click(object sender, EventArgs e)
        {
            LoadForm(new FormJenisSampah(), sender);
        }

        private void btnDataSampah_Click(object sender, EventArgs e)
        {
            LoadForm(new FormDataSampah(), sender);
        }

        private void btnChatBot_Click(object sender, EventArgs e)
        {
            LoadForm(new FormChatbot(), sender);
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDataPenjemputan_Click(object sender, EventArgs e)
        {
            LoadForm(new DataPenjemputan(), sender);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                ResetButtons(panel1);

                Button btn = (Button)btnSender;
                btn.BackColor = Color.CornflowerBlue;
                btn.ForeColor = Color.White;
            }
        }

        private void ResetButtons(Control container)
        {
            foreach (Control c in container.Controls)
            {
                if (c is Button)
                {
                    c.BackColor = SystemColors.ActiveCaption; ; 
                    c.ForeColor = Color.White;
                }

                if (c.HasChildren)
                {
                    ResetButtons(c);
                }
            }
        }
    }
}
