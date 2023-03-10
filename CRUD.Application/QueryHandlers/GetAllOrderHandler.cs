using AutoMapper;
using MediatR;
using CRUD.Application.Queries;
using CRUD.Application.Responses;
using CRUD.Application.Services;
using CRUD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRUD.Application.QueryHandlers
{
    public class GetAllOrderHandler : IRequestHandler<GetAllOrderQuery, OrderResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public GetAllOrderHandler(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OrderResponse> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {

            var results = await _orderService.GetAllAsync();

            var orderResponse = new OrderResponse() { 
                Orders= _mapper.Map<IEnumerable<OrderDto>>(results)
            };

            return orderResponse;
        }
    }
}
