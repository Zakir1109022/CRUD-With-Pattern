using MediatR;
using CRUD.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Application.Commands
{
   public class LoginCommand : IRequest<TokenResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
