using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using tachy1.BusinessLogicLayer.Services.Interfaces;
using tachy1.Models;
using tachy1.BusinessLogicLayer.Services;

namespace tachy1.BusinessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _repository = null;
        public OrderService(IOptions<DatabaseSettings> settings)
        {
            _repository = new OrderRepository(settings);
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _repository.orders.Find(x => true).ToListAsync();
        }

        public async Task<Order> GetOrderByName(string name)
        {
            var filter = Builders<Order>.Filter.Eq("Name", name);
            return await _repository.orders.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Order> GetOrderById(string id)
        {
            ObjectId ObjectedId = new ObjectId(id);
            var filter = Builders<Order>.Filter.Eq("_id", ObjectedId);
            return await _repository.orders.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddOrder(Order model)
        {
            //inserting data
            await _repository.orders.InsertOneAsync(model);
        }

        public async Task<bool> Update(string id, Order model)
        {

            ObjectId ObjectedId = new ObjectId(id);
            var filter = Builders<Order>.Filter.Eq("_id", ObjectedId);
            var order = _repository.orders.Find(filter).FirstOrDefaultAsync();
            if (order.Result == null)
                return false;
            var update = Builders<Order>.Update
                                          .Set(x => x.IsAccepted, model.IsAccepted)
                                          .Set(x => x.UpdatedOn, model.UpdatedOn);

            await _repository.orders.UpdateOneAsync(filter, update);
            return true;
        }

        public async Task<DeleteResult> RemoveOrder(string id)
        {
            ObjectId ObjectedId = new ObjectId(id);
            var filter = Builders<Order>.Filter.Eq("_id", ObjectedId);
            return await _repository.orders.DeleteOneAsync(filter);
        }
        public async Task<DeleteResult> RemoveAllOrders()
        {
            return await _repository.orders.DeleteManyAsync(new BsonDocument());
        }

       
    }
}