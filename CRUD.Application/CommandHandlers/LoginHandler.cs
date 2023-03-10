using AutoMapper;
using MediatR;
using CRUD.Application.Commands;
using CRUD.Application.Responses;
using CRUD.Application.Services;
using CRUD.Core.Dtos;
using CRUD.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRUD.Application.CommandHandlers
{
   public class LoginHandler : IRequestHandler<LoginCommand, TokenResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAccessTokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IPasswordHasherService _passwordHasherService;

        public LoginHandler(IMapper mapper, IAccessTokenService tokenService, IUserService userService, IPasswordHasherService passwordHasherService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _passwordHasherService = passwordHasherService ?? throw new ArgumentNullException(nameof(passwordHasherService));
        }

        public async Task<TokenResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            TokenResponse response = new TokenResponse();

            //Validate user
            var users = await _userService.FilterByAsync(x => x.Email == request.Email);

            var userDtos = _mapper.Map<List<UserDto>>(users);

            if (userDtos.Count == 1 && _passwordHasherService.VerifyPassword(userDtos[0].Password,request.Password))
            {
                var accessToken = _tokenService.GenerateJSONWebToken(userDtos[0]);
                var refreshToken = _tokenService.GenerateRefreshToken();

                //Update user
                var user = _mapper.Map<User>(userDtos[0]);
                user.RefreshToken = refreshToken;
                await _userService.UpdateAsync(user);

                //Set response
                response.AccessToken = accessToken;
                response.RefreshToken = refreshToken;
            }

            return response;
        }

    }
}
