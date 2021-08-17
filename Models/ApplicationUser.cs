using System;
using AspNetCore.Identity.Mongo;
using tachy1.Models.Interfaces;

namespace tachy1.Models
{
    //Add any custom field for a user
    public class ApplicationUser : MongoIdentityUser
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public DateTime? Birthdate { get; set; }
    }
}