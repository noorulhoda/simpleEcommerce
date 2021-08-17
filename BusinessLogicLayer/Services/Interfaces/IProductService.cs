

using tachy1.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace tachy1.BusinessLogicLayer.Services.Interfaces

{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductByName(string name);
        Task<Product> GetProductById(string id);
        Task AddProduct(Product model);
        Task<bool> UpdatePrice(string id,Product model);
        Task<DeleteResult> RemoveProduct(string id);
        Task<DeleteResult> RemoveAllProducts();
    }
}