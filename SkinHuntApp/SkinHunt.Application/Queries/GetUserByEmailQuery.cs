using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkinHunt.Application.Common.Entities;
using SkinHunt.Application.Common.Models;

namespace SkinHunt.Application.Queries
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public string Email { get; set; }

        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }
    }

    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;

        public GetUserByEmailQueryHandler(
            DbContext dbContext,
            IMapper mapper,
            UserManager<UserEntity> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .FirstAsync(o => o.Email == request.Email);

            var roles = await _userManager.GetRolesAsync(await _dbContext.Users.FindAsync(result.Id));
            result.Roles = roles.ToArray();

            return result;
        }
    }
}
