using Catalog.API.Entities;
using Catalog.API.Helpers;
using Catalog.API.Models.ConfigurationModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly  IConfiguration _configuration;
        private readonly ProductDatabaseSettings _pdbSettings;
        private readonly ILogger<CatalogContext> _logger;
        public CatalogContext(IConfiguration configuration, ILogger<CatalogContext> logger)
        {
            _logger = logger;
            _logger.LogInformation("Started");
            _configuration = configuration;
            _pdbSettings = new ProductDatabaseSettings() { DatabaseName = "ProductsDb", Collection = "Products", ConnectionString = "mongodb://catalogdb:27017" };
            //_pdbSettings = pdbSettings;
            var client = new MongoClient(configuration.GetValue<string>("ProductDatabaseSettings:ConnectionString"));
            var db = client.GetDatabase(configuration.GetValue<string>("ProductDatabaseSettings:DatabaseName"));
            Products = db.GetCollection<Product>(configuration.GetValue<string>("ProductDatabaseSettings:Collection"));
            SeedContext.SeedMongo(Products);
            
        }

        public IMongoCollection<Product> Products { get; set; }
    }
}
