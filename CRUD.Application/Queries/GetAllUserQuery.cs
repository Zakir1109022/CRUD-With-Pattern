using MediatR;
using CRUD.Application.Responses;
using CRUD.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Application.Queries
{
   public class GetAllUserQuery : IRequest<Response<UserDto>>
    {

    }
}
