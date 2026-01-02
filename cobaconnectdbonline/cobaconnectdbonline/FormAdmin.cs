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

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackgroundImageLayout = ImageLayout.None;
        }


        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }
        private void LoadForm(Form frm)
        {
            panelMain.Controls.Clear();

            frm.TopLevel = false;
            frm.FormBorderStyle = FormBorderStyle.None;
            frm.Dock = DockStyle.Fill;

            panelMain.Controls.Add(frm);
            frm.Show();
        }


        private void btnKabko_Click(object sender, EventArgs e)
        {
            LoadForm(new FormKabupatenKota());
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            LoadForm(new FormDataUser());
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
            LoadForm(new FormJenisSampah());
        }

        private void btnDataSampah_Click(object sender, EventArgs e)
        {
            LoadForm(new FormDataSampah());
        }

        private void btnChatBot_Click(object sender, EventArgs e)
        {
            LoadForm(new FormChatbot());
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
