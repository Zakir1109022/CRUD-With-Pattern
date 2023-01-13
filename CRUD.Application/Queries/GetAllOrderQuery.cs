using MediatR;
using CRUD.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Application.Queries
{
    public class GetAllOrderQuery : IRequest<OrderResponse>
    {
      
    }
}
