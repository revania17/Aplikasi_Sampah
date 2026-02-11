using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace cobaconnectdbonline.Models
{
    public class DataSampah
    {
        internal double latitude;
        internal double longitude;
        internal object nama;
        internal object status;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id_sampah { get; set; }

        public string id_wilayah { get; set; }  
        public string id_jenis { get; set; }   

        public double Jumlah { get; set; }
        public DateTime Tanggal { get; set; }
        public string Petugas { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Id { get; internal set; }
        public double Latitude { get; internal set; }
        public double Longitude { get; internal set; }
        public object Nama { get; internal set; }
        public object Status { get; internal set; }
    }

}
