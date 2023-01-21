using AutoMapper;
using MediatR;
using CRUD.Application.Commands;
using CRUD.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRUD.Application.CommandHandlers
{

   public class SentEmailHandler : IRequestHandler<SentEmailCommand, bool>
    {
        private readonly IMapper _mapper;

        public SentEmailHandler(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task<bool> Handle(SentEmailCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Email sent successfully");
            
            return true;
        }

    }
}
