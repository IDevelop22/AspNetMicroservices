using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderList
{
    public class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, List<OrdersVm>>
    {
        private readonly IOrderRepository _repo;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IMapper mapper, IOrderRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<List<OrdersVm>> Handle(GetOrdersListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repo.GetOrdersByUsername(request.Username);
            return _mapper.Map<List<OrdersVm>>(orders);
        }
    }
}
