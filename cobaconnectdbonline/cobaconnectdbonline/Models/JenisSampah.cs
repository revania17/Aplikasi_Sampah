using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace cobaconnectdbonline.Models
{
    public class JenisSampah
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id_jenis { get; set; }

        [BsonElement("nama_jenis")]
        public string nama_jenis { get; set; }

        [BsonElement("keterangan")]
        public string keterangan { get; set; }
    }
}
