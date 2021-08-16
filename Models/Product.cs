using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace tachy1.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

    }
}