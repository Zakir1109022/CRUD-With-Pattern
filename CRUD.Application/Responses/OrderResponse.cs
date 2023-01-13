using CRUD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Application.Responses
{
    public class OrderResponse
    {
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
