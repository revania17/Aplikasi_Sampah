using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace cobaconnectdbonline.Models
{
    [BsonIgnoreExtraElements]
    public class Data_Penjemputan
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nama")]
        public string Nama { get; set; }

        [BsonElement("latitude")]
        public double Latitude { get; set; }

        [BsonElement("longitude")]
        public double Longitude { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("tanggal")]
        public DateTime? Tanggal { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
