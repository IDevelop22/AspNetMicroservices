using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepo : IDiscountRepo
    {
        private readonly ILogger<DiscountRepo> _logger;
        private readonly DbConnection _dbConn;
        public DiscountRepo(ILogger<DiscountRepo> logger,IConfiguration config)
        {
            _logger = logger;
            _dbConn = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
        }

        public async Task<bool> AddCoupon(Coupon coupon)
        {
            var result = await _dbConn.ExecuteAsync("INSERT INTO Coupon (productname,description,amount) VALUES(@ProductName,@Description,@Amount)",new { 
             ProductName = coupon.ProductName,
             Description = coupon.Description,
             Amount = coupon.Amount
            });

            return result > 0;
        }

        public async Task<Coupon> GetCoupon(string productName)
        {
            var coupon = await _dbConn.QueryFirstOrDefaultAsync<Coupon>("Select * FROM Coupon WHERE productName=@ProductName ", new { ProductName = productName });
            return coupon;
        }

        public async Task<bool> RemoveCoupon(string productName)
        {
            var rows = await _dbConn.ExecuteAsync("DELETE FROM Coupon Where productName=@ProductName", new { ProductName = productName });
            return rows > 1;
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            var rows = await _dbConn.ExecuteAsync(@"UPDATE Coupon SET
                                                        productName=@ProductName,
                                                        description=@Description,
                                                        amount=@Amount", new
                                                                    {
                                                                        ProductName = coupon.ProductName,
                                                                        Description = coupon.Description,
                                                                        Amount = coupon.Amount
                                                                    });
            return rows > 0;
        }
    }
}
