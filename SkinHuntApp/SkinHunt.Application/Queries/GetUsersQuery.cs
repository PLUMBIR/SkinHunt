using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkinHunt.Application.Common.Entities;
using SkinHunt.Application.Common.Models;
using SkinHunt.Domain.Constants;

namespace SkinHunt.Application.Queries
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUsersQueryHandler> _logger;
        private readonly UserManager<UserEntity> _userManager;

        public GetUsersQueryHandler(DbContext dbContext, IMapper mapper, ILogger<GetUsersQueryHandler> logger, UserManager<UserEntity> userManager = null)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var adminUsers = await _userManager.GetUsersInRoleAsync(RolesConstants.Admin);
                var adminUsersIdList = adminUsers.Select(x => x.Id).ToList();

                var users = await _dbContext.Users
                    .Where(o => !adminUsersIdList.Contains(o.Id))
                    .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                if (users.Any())
                {
                    _logger.LogInformation("Get users was succeeded.");
                    return users.ToList();
                }

                return new List<UserDto>();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Get users failed with exception: {ex.Message}");
                return new List<UserDto>();
            }
        }
    }
}
