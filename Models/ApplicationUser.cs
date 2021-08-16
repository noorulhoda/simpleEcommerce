using System;
using AspNetCore.Identity.Mongo;

namespace tachy1.Models
{
    //Add any custom field for a user
    public class ApplicationUser : MongoIdentityUser
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public DateTime? Birthdate { get; set; }

        public string Country { get; set; }

    
    }
}