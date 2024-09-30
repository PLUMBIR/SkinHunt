using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkinHunt.Application.Common.Models;

namespace SkinHunt.Application.Queries
{
    public class GetSkinsByUserIdFromBasketQuery : IRequest<List<BasketDto>>
    {
        public string Id { get; set; }
    }

    public class GetSkinsByUserIdQueryHandler : IRequestHandler<GetSkinsByUserIdFromBasketQuery, List<BasketDto>>
    {
        private readonly DbContext _db;
        private readonly ILogger<GetSkinsByUserIdQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetSkinsByUserIdQueryHandler(DbContext db, ILogger<GetSkinsByUserIdQueryHandler> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<BasketDto>> Handle(GetSkinsByUserIdFromBasketQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _db.Basket.Where(o => o.User.Id == request.Id)
                    .Include(o => o.User)
                    .Include(o => o.Skin)
                    .ProjectTo<BasketDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                if (result.Any())
                {
                    _logger.LogInformation("Skins retrieved successfully.");
                    return result;
                }

                return new List<BasketDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving skins.");
                return new List<BasketDto>();
            }
        }
    }
}
