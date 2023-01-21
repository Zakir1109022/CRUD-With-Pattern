using MediatR;
using CRUD.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Application.Commands
{
   public class SentEmailCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }
}
