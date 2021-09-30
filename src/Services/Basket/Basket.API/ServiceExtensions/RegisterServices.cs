using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.ServiceExtensions
{
    public static class RegisterServices
    {
        public static void RegisterRedis(this IServiceCollection services, IConfiguration config )
        {
            services.AddStackExchangeRedisCache(opts =>
            {
                opts.Configuration = config.GetValue<string>("CacheSettings:ConnectionString");
            });
        }
    }
}
