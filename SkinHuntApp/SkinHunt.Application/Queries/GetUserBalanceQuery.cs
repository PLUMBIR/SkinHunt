using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkinHunt.Application.Queries
{
    public class GetUserBalanceQuery : IRequest<double>
    {
        public string Id { get; set; }

        public GetUserBalanceQuery(string id)
        {
            Id = id;
        }
    }

    public class GetUserBalanceQueryHandler : IRequestHandler<GetUserBalanceQuery, double>
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<GetUserBalanceQueryHandler> _logger;

        public GetUserBalanceQueryHandler(DbContext dbContext, ILogger<GetUserBalanceQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<double> Handle(GetUserBalanceQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var balance = await _dbContext.Users
                    .Where(o => o.Id == request.Id)
                    .Select(o => o.Balance)
                    .FirstAsync();

                return balance;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Get users failed with exception: {ex.Message}");
                return 0;
            }
        }
    }
}
