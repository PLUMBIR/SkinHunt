using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkinHunt.Application.Common.Models;

namespace SkinHunt.Application.Queries
{

    public class GetSkinsFromBasketQuery : IRequest<List<BasketDto>>
    {
        public string Id { get; set; }

        public GetSkinsFromBasketQuery(string id)
        {
            Id = id;
        }
    }

    public class GetSkinsFromBasketQueryHandler : IRequestHandler<GetSkinsFromBasketQuery, List<BasketDto>>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<GetSkinsFromBasketQueryHandler> _logger;

        public GetSkinsFromBasketQueryHandler(DbContext dbContext, IMapper mapper, ILogger<GetSkinsFromBasketQueryHandler> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<BasketDto>> Handle(GetSkinsFromBasketQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var skins = await _dbContext.Basket
                    .ProjectTo<BasketDto>(_mapper.ConfigurationProvider)
                    .Where(o => o.User.Id == request.Id)
                    .ToListAsync();

                if (skins.Any())
                {
                    _logger.LogInformation("Get users was succeeded.");
                    return skins;
                }

                return new List<BasketDto>();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Get users failed with exception: {ex.Message}");
                return new List<BasketDto>();
            }
        }
    }
}
