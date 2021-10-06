using AutoMapper;
using Discount.grpc.Protos;
using Discount.grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ILogger<DiscountService> _logger;
        private readonly IDiscountRepo _repo;
        private readonly IMapper _mapper;

        public DiscountService(ILogger<DiscountService> logger, IDiscountRepo repo, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountModel request, ServerCallContext context)
        {
            _logger.LogInformation($"Request Starting : {request.ProductName}");
            var coupon = await _repo.GetCoupon(request.ProductName);
            var couponModel = _mapper.Map<CouponModel>(coupon);
            _logger.LogInformation($"Before Return : {request.ProductName}");
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountModel request, ServerCallContext context)
        {
            return null;
        }
    }
}
