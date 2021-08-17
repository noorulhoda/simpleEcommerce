using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tachy1.Models;

namespace tachy1.BusinessLogicLayer.Repositories
{
    public class ProductRepository
    {

        //delcaring mongo db
        private readonly IMongoDatabase _database;

        public ProductRepository(IOptions<DatabaseSettings> settings)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                if (client != null)
                    _database = client.GetDatabase(settings.Value.Database);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to MongoDb server.", ex);
            }

        }

        public IMongoCollection<Product> products => _database.GetCollection<Product>("Products");

    }
}
    