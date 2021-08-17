using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tachy1.Models;

namespace tachy1.BusinessLogicLayer.Services.Interfaces
{
 
        public interface IOrderService
        {
            Task<IEnumerable<Order>> GetAllOrders();
            Task<Order> GetOrderByName(string name);
        Task<Order> GetOrderById(string id);
        Task AddOrder(Order model);
            Task<bool> Update(string id,Order model);
            Task<DeleteResult> RemoveOrder(string name);
            Task<DeleteResult> RemoveAllOrders();
        }
    
}
