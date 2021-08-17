
using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tachy1.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public ICollection<Product> WishedProducts { get; set; }
        public ICollection<Order> Orders { get; set; }
       // public List<ObjectId> OrdersIds { get; set; }
    }

    public enum Role
    {
        Admin=1,
        User=2
    }
}