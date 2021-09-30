using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepo(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<ShoppingCart> GetCart(string username)
        {
            var cartString =await  _redisCache.GetStringAsync(username);
            ShoppingCart cart = null; ;
            if (!String.IsNullOrEmpty(cartString))
            {
                cart = JsonConvert.DeserializeObject<ShoppingCart>(cartString);
            }

            return cart ?? new ShoppingCart(username);
        }

        public async Task RemoveCart(string username)
        {
            await _redisCache.RemoveAsync(username);
        }

        public async Task<ShoppingCart> UpdateCart( ShoppingCart cart)
        {
            await _redisCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
            return await  GetCart(cart.UserName);
        }
    }
}
