using AspNetCore.Identity.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;




namespace tachy1.Models.Interfaces
{
    //Add any custom field for a user
    public class IApplicationUser : MongoIdentityUser
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public DateTime? Birthdate { get; set; }

        public string Country { get; set; }


    }
}