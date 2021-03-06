using AutoMapper;
using Ordering.Application.Features.Orders.Queries.GetOrderList;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Mappings
{
    class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Order, OrdersVm>().ReverseMap();
        }
    }
}
