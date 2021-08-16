

using tachy1.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace tachy1.BusinessLogicLayer.Services.Interfaces

{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProduct(string name);
        Task AddProduct(Product model);
        Task<bool> UpdatePrice(Product model);
        Task<DeleteResult> RemoveProduct(string name);
        Task<DeleteResult> RemoveAllProducts();
    }
}