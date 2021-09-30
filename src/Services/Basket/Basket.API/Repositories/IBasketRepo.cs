using Basket.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepo
    {
        public Task<ShoppingCart> GetCart(string username);
        public Task<ShoppingCart> UpdateCart(ShoppingCart cart);
        public Task RemoveCart(string username);

    }
}
