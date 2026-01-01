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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Database db = new Database();

            var user = db.Users.Find(u => u.email == txtEmail.Text).FirstOrDefault();

            if (user == null)
            {
                MessageBox.Show("Email tidak ditemukan");
                return;
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(txtPassword.Text, user.password);

            if (!isValid)
            {
                MessageBox.Show("Password salah");
                return;
            }

            MessageBox.Show("Login berhasil sebagai " + user.role);

            // Contoh role
            if (user.role == "Admin")
            {
                new FormAdmin().Show();
            }
            else
            {
                new FormPetugas().Show();
            }

            this.Hide();
        }

        private void btnRegis_Click(object sender, EventArgs e)
        {
            new FormRegis().Show();
            this.Hide();
        }

        //private void TestMongoConnection()
        //{
        //    try
        //    {
        //        string connectionString =
        //        "mongodb+srv://bilaismina:Smp12345@sampah-cluster.6w4au7b.mongodb.net/?appName=Sampah-cluster";

        //        var client = new MongoClient(connectionString);

        //        // Ping ke server
        //        var database = client.GetDatabase("admin");
        //        var command = new MongoDB.Bson.BsonDocument("ping", 1);
        //        database.RunCommand<MongoDB.Bson.BsonDocument>(command);

        //        MessageBox.Show("✅ Koneksi ke MongoDB BERHASIL");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("❌ Koneksi GAGAL:\n" + ex.Message);
        //    }
        //}


    }
}
