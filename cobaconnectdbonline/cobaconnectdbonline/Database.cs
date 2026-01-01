using cobaconnectdbonline.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cobaconnectdbonline
{
    public class Database
    {
        private MongoClient client;
        private IMongoDatabase database;

        public Database()
        {
            client = new MongoClient(
            "mongodb+srv://rahmaaul:Smp12345@sampah-cluster.6w4au7b.mongodb.net/?appName=Sampah-cluster");

            database = client.GetDatabase("db_sampah");
        }

        public IMongoCollection<User> Users =>
            database.GetCollection<User>("users");

        public IMongoCollection<KabupatenKota> Kabupaten =>
            database.GetCollection<KabupatenKota>("kabupaten_kota");

        public IMongoCollection<JenisSampah> JenisSampah =>
            database.GetCollection<JenisSampah>("jenis_sampah");

        public IMongoCollection<DataSampah> DataSampah =>
            database.GetCollection<DataSampah>("data_sampah");

    }
}
