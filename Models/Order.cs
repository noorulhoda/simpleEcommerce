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

        public string Name { get; set; }

        public DateTime? OrderedOn { get; set; }

        public Boolean IsPaid { get; set; }

        public Status Status { get; set; }

        public Boolean IsAccepted { get; set; }

    }


    public enum Status
    {
        confirmed,
        pending,
        delivered

    }
}