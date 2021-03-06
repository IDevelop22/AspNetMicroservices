using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Models.ConfigurationModels
{
    public interface IProductDatabaseSettings 
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string Collection { get; set; }
    }
}
