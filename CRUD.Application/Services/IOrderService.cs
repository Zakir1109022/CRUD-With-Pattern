using CRUD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Application.Services
{
   public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(string user_name);
        Task CreateAsync(Order order);
    }
}
