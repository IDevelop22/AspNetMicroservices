using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepo _repo;

        public BasketController(IBasketRepo repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }
        [HttpGet("{username}",Name = "GetCart")]
        public async Task<ActionResult<ShoppingCart>> GetCart(string username)
        {
            var cart = await _repo.GetCart(username);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart shoppingCart)
        {
            var cart = await _repo.UpdateCart(shoppingCart);
            return Ok(cart);

        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCart(string username)
        {
            await _repo.RemoveCart(username);
            return Ok();
        }
    }
}
