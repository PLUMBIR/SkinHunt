using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkinHunt.Application.Common.Enums;
using SkinHunt.Application.Common.Models;

namespace SkinHunt.Application.Queries
{
    public class GetSkinsQuery : IRequest<List<SkinDto>>
    {
        public string SortBy { get; set; }

        public decimal? PriceAbove { get; set; }

        public decimal? PriceLess { get; set; }

        public Category[] Types { get; set; }

        public string[] Rarity {  get; set; }

        public double? FloatAbove { get; set; }

        public double? FloatLess { get; set; }
    }

    public class GetSkinsQueryHandler : IRequestHandler<GetSkinsQuery, List<SkinDto>>
    {
        private readonly DbContext _db;
        private readonly ILogger<GetSkinsQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetSkinsQueryHandler(DbContext db, ILogger<GetSkinsQueryHandler> logger, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<SkinDto>> Handle(GetSkinsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var skinIdsInBasketList = await _db.Basket
                    .Select(o => o.Skin.Id)
                    .ToListAsync();

                var skinIdsInSoldList = await _db.SoldsSkins
                    .Select(o => o.Skin.Id)
                    .ToListAsync();

                var query = _db.Skins
                    .Include(s => s.Type)
                    .Where(o => !skinIdsInBasketList.Contains(o.Id) && !skinIdsInSoldList.Contains(o.Id))
                    .ProjectTo<SkinDto>(_mapper.ConfigurationProvider);
                
                if (request.PriceAbove is not null)
                {
                    query = query.Where(o => o.PriceWithDiscount >= request.PriceAbove);
                }

                if (request.PriceLess is not null)
                {
                    query = query.Where(o => o.PriceWithDiscount <= request.PriceLess);
                }

                if (request.Types is not null)
                {
                    query = query.Where(o => request.Types.Contains(o.Type.Category));
                }

                if (request.Rarity is not null) 
                {
                    query = query.Where(o => request.Rarity.Contains(o.Rarity));
                }

                if (request.FloatAbove is not null)
                {
                    query = query.Where(o => o.Float >= request.FloatAbove);
                }

                if (request.FloatLess is not null)
                {
                    query = query.Where(o => o.Float <= request.FloatLess);
                }

                if (request.SortBy == "priceMax")
                {
                    query = query.OrderByDescending(s => s.Price);            
                }

                if (request.SortBy == "priceMin")
                {
                    query = query.OrderBy(s => s.Price);             
                }

                if (request.SortBy == "floatMax")
                {
                    query = query.OrderByDescending(s => s.Float);            
                }

                if (request.SortBy == "floatMin")
                {
                    query = query.OrderBy(s => s.Float);                 
                }

                var result = await query.ToListAsync();

                if (result.Any())
                {
                    _logger.LogInformation("Skins retrieved successfully.");
                    return result;
                }

                return new List<SkinDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving skins.");
                return new List<SkinDto>();
            }
        }
    }
}
