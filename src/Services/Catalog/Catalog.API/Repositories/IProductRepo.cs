using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductsByName(string name);
        Task<IEnumerable<Product>> GetProductsByCategory(string Category);
        Task<Product> GetProductById(string id);

        Task<bool> UpdateProduct(Product product);
        Task<Product> AddProduct(Product product);
        Task<bool> DeleteProduct(string id);
        
    }
}
