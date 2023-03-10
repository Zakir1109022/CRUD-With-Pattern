using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CRUD.Application.Commands;
using CRUD.Application.Queries;
using CRUD.Application.Responses;
using CRUD.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRUD.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<Response<UserDto>>> Register([FromBody] CreateUserCommand userInfo)
        {
            var response = await _mediator.Send(userInfo);
            return response;
        }


        [AllowAnonymous]
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromForm] LoginCommand loginInfo)
        {
            var response = await _mediator.Send(loginInfo);
            return response;
        }

        [AllowAnonymous]
        [HttpPost("token")]
        public async Task<ActionResult<TokenResponse>> Token(GenerateAccessTokenCommand tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            var response = await _mediator.Send(tokenModel);
            return response;
        }


       // [Authorize]
        [Authorize(Roles ="Admin")]
        [HttpGet("getAll")]
        public async Task<ActionResult<Response<UserDto>>> GetAll()
        {
            var query = new GetAllUserQuery();
            var orderList = await _mediator.Send(query);
            return Ok(orderList);
        }


    }
}
