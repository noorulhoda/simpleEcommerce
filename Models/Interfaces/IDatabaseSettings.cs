using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tachy1.Models
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string ProductsCollectionName { get; set; }
        string OrdersCollectionName { get; set; }
        string Database { get; set; }
    }
}
