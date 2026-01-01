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
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id_sampah { get; set; }

        public string id_wilayah { get; set; }  
        public string id_jenis { get; set; }   

        public double Jumlah { get; set; }
        public DateTime Tanggal { get; set; }
        public string Petugas { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
