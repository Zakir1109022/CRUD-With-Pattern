using AutoMapper;
using CRUD.Application.Commands;
using CRUD.Common.Events;
using CRUD.Core;
using CRUD.Core.Dtos;
using CRUD.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Order, CreateOrderCommand>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderCheckoutEvent, CreateOrderCommand>().ReverseMap();
            CreateMap<OrderCheckoutEvent, SentEmailCommand>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
