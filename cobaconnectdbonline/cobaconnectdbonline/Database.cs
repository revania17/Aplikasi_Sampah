using cobaconnectdbonline.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cobaconnectdbonline
{
    public class Database
    {
        private MongoClient client;
        private IMongoDatabase database;

        public Database()
        {
            client = new MongoClient(
            "mongodb+srv://zahra:Smp12345@sampah-cluster.6w4au7b.mongodb.net/?appName=Sampah-cluster");

            database = client.GetDatabase("db_sampah");
        }

        public async Task SeedAdmin()
        {
            var count = await Users.CountDocumentsAsync(u => true);
            if (count == 0)
            {
                var admin = new User
                {
                    email = "admin@gmail.com",
                    password = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    role = "Admin",
                    nama = "Super Admin"
                };
                await Users.InsertOneAsync(admin);
                MessageBox.Show("Database Kosong. Admin Default Berhasil Dibuat!");
            }
        }

        public IMongoCollection<User> Users =>
            database.GetCollection<User>("users");

        public IMongoCollection<KabupatenKota> Kabupaten =>
            database.GetCollection<KabupatenKota>("kabupaten_kota");

        public IMongoCollection<JenisSampah> JenisSampah =>
            database.GetCollection<JenisSampah>("jenis_sampah");

        public IMongoCollection<DataSampah> DataSampah =>
            database.GetCollection<DataSampah>("data_sampah");

        public IMongoCollection<Data_Penjemputan> Data_Penjemputan =>
            database.GetCollection<Data_Penjemputan>("data_penjemputan");

        public IMongoCollection<BsonDocument> KnowledgeSampah =>
            database.GetCollection<BsonDocument>("knowledge_sampah");

    }
}
