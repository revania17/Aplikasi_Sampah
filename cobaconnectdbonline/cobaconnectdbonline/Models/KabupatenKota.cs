using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cobaconnectdbonline.Models
{
    public class KabupatenKota
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id_wilayah { get; set; }

        public string nama_wilayah { get; set; }
        public string provinsi { get; set; }
    }
}
