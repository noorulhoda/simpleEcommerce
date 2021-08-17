using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace tachy1.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Boolean IsPaid { get; set; }

        public Status Status { get; set; }

        public Boolean IsAccepted { get; set; }

        public Product Product { get; set; }

    }


    public enum Status
    {
        confirmed,
        pending,
        delivered

    }
}