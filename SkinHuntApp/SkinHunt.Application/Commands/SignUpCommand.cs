using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkinHunt.Application.Common.Entities;
using SkinHunt.Application.Common.Interfaces;
using SkinHunt.Application.Common.Models;
using SkinHunt.Domain.Constants;
using SkinHunt.Domain.Models;

namespace SkinHunt.Application.Commands
{
    public class SignUpCommand : IRequest<UserDto>
    {
        public SignUpModel Model { get; set; }

        public SignUpCommand(SignUpModel model)
        {
            Model = model;
        }
    }

    public class SignUpCommandHandler : IRequestHandler<SignUpCommand, UserDto>
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IJwtExtension _jwtExtension;
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public SignUpCommandHandler(
            UserManager<UserEntity> userManager,
            IJwtExtension jwtExtension,
            DbContext dbContext,
            IMapper mapper)
        {
            _userManager = userManager;
            _jwtExtension = jwtExtension;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = new UserEntity
            {
                UserName = request.Model.Email,
                Email = request.Model.Email,
                PhoneNumber = request.Model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, request.Model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, RolesConstants.User);

                var userDto = await _dbContext.Users
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .FirstAsync(o => o.UserName == user.UserName);
                
                var token = await _jwtExtension.GenerateTokenAsync(user);
                userDto.Token = token;

                var roles = await _userManager.GetRolesAsync(user);
                userDto.Roles = roles.ToArray();

                return userDto;
            }

            return null;
        }
    }
}
