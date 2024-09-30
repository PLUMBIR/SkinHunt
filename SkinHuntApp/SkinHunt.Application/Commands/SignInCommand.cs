using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkinHunt.Application.Common.Entities;
using SkinHunt.Application.Common.Interfaces;
using SkinHunt.Application.Common.Models;

namespace SkinHunt.Application.Commands
{
    public class SignInCommand : IRequest<UserDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public SignInCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }

    public class SignInCommandHandler : IRequestHandler<SignInCommand, UserDto>
    {
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtExtension _jwtExtension;
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public SignInCommandHandler(
            SignInManager<UserEntity> signInManager,
            IJwtExtension jwtExtension,
            DbContext dbContext,
            IMapper mapper,
            UserManager<UserEntity> userManager)
        {
            _signInManager = signInManager;
            _jwtExtension = jwtExtension;
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserDto> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, true);

            if (result.Succeeded)
            {
                var user = await _dbContext.Users.FirstAsync(o => o.UserName == request.UserName);
                
                var token = await _jwtExtension.GenerateTokenAsync(user);

                var userDto = await _dbContext.Users
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .FirstAsync(o => o.UserName == request.UserName);

                userDto.Token = token;

                var roles = await _userManager.GetRolesAsync(user);
                userDto.Roles = roles.ToArray();

                return userDto;
            }

            return null;
        }
    }
}
