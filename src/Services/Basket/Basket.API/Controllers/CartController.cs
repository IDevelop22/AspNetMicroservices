using Basket.API.Entities;
using Basket.API.Repositories;
using Discount.grpc.Protos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountService;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketRepo repo, DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient, ILogger<BasketController> logger)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _discountService = discountProtoServiceClient;
            _logger = logger;
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
            foreach (var prod in shoppingCart.Items)
            {
                _logger.LogInformation($"Starting(Basket) {prod.ProductName}");
                var couponModel = await _discountService.GetDiscountAsync(new Discount.grpc.Protos.GetDiscountModel() { ProductName = prod.ProductName });
                if (couponModel != null) _logger.LogInformation($"Got Coupon {couponModel.Description}");
                _logger.LogInformation($"Ending(Basket) {prod.ProductName}");
                // prod.Price -= couponModel.Amount;

            }
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
