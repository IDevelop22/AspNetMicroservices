using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepo _repo;

        public DiscountController(IDiscountRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<Coupon>> GetCoupon(string productName)
        {
            var coupon = await _repo.GetCoupon(productName);
            return coupon;
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddCoupon(Coupon coupon)
        {
            var result = await _repo.AddCoupon(coupon);
            return result;
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateCoupon(Coupon coupon)
        {
            var result = await _repo.UpdateCoupon(coupon);
            return result;
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteCoupon(string productName)
        {
            var result = await _repo.RemoveCoupon(productName);
            return result;
        }
    }
}
