using AutoMapper;
using MediatR;
using CRUD.Application.Commands;
using CRUD.Application.Responses;
using CRUD.Application.Services;
using CRUD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CRUD.Application.CommandHandlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderResponse>
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public CreateOrderHandler(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<OrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            var order = new Order() {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                EmailAddress=request.EmailAddress
            };

             await _orderService.CreateAsync(order);

            List<OrderDto> orderDtoList = new List<OrderDto>();
            var orderDto = _mapper.Map<OrderDto>(order);
            orderDtoList.Add(orderDto);

            OrderResponse response = new OrderResponse() {
                Orders = orderDtoList
            };


            return response;
        }
    }
}
