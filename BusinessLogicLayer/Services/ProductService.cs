using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using tachy1.BusinessLogicLayer.Services.Interfaces;
using tachy1.Models;
using tachy1.DataLayer;

namespace tachy1.BusinessLogicLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepository _repository = null;
        public ProductService(IOptions<DatabaseSettings> settings)
        {
            _repository = new ProductRepository(settings);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _repository.products.Find(x => true).ToListAsync();
        }

        public async Task<Product> GetProduct(string name)
        {
            var filter = Builders<Product>.Filter.Eq("Name", name);
            return await _repository.products.Find(filter).FirstOrDefaultAsync();
        }
        public async Task AddProduct(Product model)
        {
            //inserting data
            await _repository.products.InsertOneAsync(model);
        }

        public async Task<bool> UpdatePrice(Product model)
        {

            var filter = Builders<Product>.Filter.Eq("Name", model.Name);
            var product = _repository.products.Find(filter).FirstOrDefaultAsync();
            if (product.Result == null)
                return false;
            var update = Builders<Product>.Update
                                          .Set(x => x.Price, model.Price)
                                          .Set(x => x.UpdatedOn, model.UpdatedOn);

            await _repository.products.UpdateOneAsync(filter, update);
            return true;
        }

        public async Task<DeleteResult> RemoveProduct(string name)
        {
            var filter = Builders<Product>.Filter.Eq("Name", name);
            return await _repository.products.DeleteOneAsync(filter);
        }
        public async Task<DeleteResult> RemoveAllProducts()
        {
            return await _repository.products.DeleteManyAsync(new BsonDocument());
        }


    }
}