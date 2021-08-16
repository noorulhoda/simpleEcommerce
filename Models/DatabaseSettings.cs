using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tachy1.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ProductsCollectionName { get; set; }
        public string OrdersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }

}
