using MediatR;
using Microsoft.Extensions.Logging;

namespace SkinHunt.Application.Queries
{
    public class GetSoldsCountQuery : IRequest<int>
    {
        public string UserId { get; set; }

        public GetSoldsCountQuery(string id)
        {
            UserId = id;
        }
    }

    public class GetSoldsCountQueryHandler : IRequestHandler<GetSoldsCountQuery, int>
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<GetSoldsCountQueryHandler> _logger;

        public GetSoldsCountQueryHandler(DbContext dbContext, ILogger<GetSoldsCountQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> Handle(GetSoldsCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var skinsCount = _dbContext.SoldsSkins
                    .Where(o => o.User.Id == request.UserId)
                    .Count();

                _logger.LogInformation("Get skins count was succeeded.");
                return skinsCount;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get skins count failed with exception: {ex.Message}");
                return 0;
            }
        }
    }
}
