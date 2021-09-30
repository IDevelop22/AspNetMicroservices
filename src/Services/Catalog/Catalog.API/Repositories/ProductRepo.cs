using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class ProductRepo : IProductRepo
    {
        private readonly ICatalogContext _context;

        public ProductRepo(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id ,id);
            var del = await _context.Products.DeleteOneAsync(filter);
            return del.IsAcknowledged && del.DeletedCount > 0;

        }

        public async Task<Product> GetProductById(string id)
        {
            return await _context
                            .Products
                            .Find(p => p.Id == id)
                            .FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context
                             .Products
                             .Find(p => true)
                             .ToListAsync();
                             
                    
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string Category)
        {
            return await _context.Products.Find(p => p.Category == Category).ToListAsync();
        }

        public async Task<Product> GetProductsByName(string name)
        {
            return await _context.Products.Find(p => p.Name == name).FirstOrDefaultAsync(); ;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, product.Id);
            var up = await _context.Products.ReplaceOneAsync(filter, product);
            return up.IsAcknowledged && up.ModifiedCount > 0;
        }
    }
}
