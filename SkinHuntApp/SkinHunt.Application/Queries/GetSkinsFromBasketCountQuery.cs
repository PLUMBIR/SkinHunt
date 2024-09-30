using MediatR;
using Microsoft.Extensions.Logging;

namespace SkinHunt.Application.Queries
{
    public class GetSkinsFromBasketCountQuery : IRequest<int>
    {
        public string UserId { get; set; }

        public GetSkinsFromBasketCountQuery(string id)
        {
            UserId = id;
        }
    }

    public class GetSkinsFromBasketCountQueryHandler : IRequestHandler<GetSkinsFromBasketCountQuery, int>
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<GetSkinsFromBasketCountQueryHandler> _logger;

        public GetSkinsFromBasketCountQueryHandler(DbContext dbContext, ILogger<GetSkinsFromBasketCountQueryHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> Handle(GetSkinsFromBasketCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var skinsCount = _dbContext.Basket
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
