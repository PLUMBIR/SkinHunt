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
    public class GetTransactionsQuery : IRequest<List<TransactionsDto>>
    {
        public string UserId { get; set; }
    }

    public class GetTransactionsQueryHandler : IRequestHandler<GetTransactionsQuery, List<TransactionsDto>>
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<GetTransactionsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;

        public GetTransactionsQueryHandler(DbContext dbContext, ILogger<GetTransactionsQueryHandler> logger, IMapper mapper, UserManager<UserEntity> userManager)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<List<TransactionsDto>> Handle(GetTransactionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var adminUsers = await _userManager.GetUsersInRoleAsync(RolesConstants.Admin);
                var adminUsersIdList = adminUsers.Select(x => x.Id).ToList();

                var query = _dbContext.SoldsSkins
                    .Include(u => u.User)
                    .Include(s => s.Skin)
                    .ProjectTo<TransactionsDto>(_mapper.ConfigurationProvider);

                if (!adminUsersIdList.Contains(request.UserId))
                {
                    query = query.Where(u => u.User.Id == request.UserId);
                }

                var transactions = await query.ToListAsync();

                return transactions;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get users failed with exception: {ex.Message}");
                return new List<TransactionsDto>();
            }
        }
    }
}
