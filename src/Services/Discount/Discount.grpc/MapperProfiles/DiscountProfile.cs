using AutoMapper;
using Discount.grpc.Entities;
using Discount.grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.grpc.MapperProfiles
{
    public class DiscountProfile :Profile
    {
        public DiscountProfile()
        {
            CreateMap<CouponModel, Coupon>().ReverseMap();
        }
    }
}
