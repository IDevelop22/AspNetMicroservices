﻿using Discount.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public interface IDiscountRepo
    {
        Task<Coupon> GetCoupon(string productName);
        Task<bool> UpdateCoupon(Coupon coupon);
        Task<bool> AddCoupon(Coupon coupon);
        Task<bool> RemoveCoupon(string ProductName);
    }
}
