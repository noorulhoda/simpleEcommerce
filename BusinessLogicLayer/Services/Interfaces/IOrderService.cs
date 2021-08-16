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
            Task<Order> GetOrder(string name);
            Task AddOrder(Order model);
            Task<bool> UpdatePrice(Order model);
            Task<DeleteResult> RemoveOrder(string name);
            Task<DeleteResult> RemoveAllOrders();
        }
    
}
