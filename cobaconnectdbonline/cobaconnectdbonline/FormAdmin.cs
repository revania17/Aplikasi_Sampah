using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cobaconnectdbonline
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }

        private void btnKabko_Click(object sender, EventArgs e)
        {
            new FormKabupatenKota().Show();
            this.Hide();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            new FormDataUser().Show(); 
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin ingin logout?",
        "Konfirmasi",
        MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Hide();
                Form1 login = new Form1();
                login.Show();
            }
        }
    }
}
