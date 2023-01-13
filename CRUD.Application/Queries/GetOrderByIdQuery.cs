using MediatR;
using CRUD.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderResponse>
    {
        public string Id { get; set; }

        public GetOrderByIdQuery(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}
