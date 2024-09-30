using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SkinHunt.Application.Common.Entities;
using SkinHunt.Application.Common.Models;

namespace SkinHunt.Application.Commands
{
    public class BuySkinCommand : IRequest<bool>
    {
        public string UserId { get; set; }

        public Guid SkinId { get; set; }

        public BuySkinCommand(SoldModel model)
        {
            UserId = model.UserId;
            SkinId = model.SkinId;
        }
    }

    public class BuySkinCommandHandler : IRequestHandler<BuySkinCommand, bool>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<BuySkinCommandHandler> _logger;

        public BuySkinCommandHandler(DbContext dbContext, IMapper mapper, ILogger<BuySkinCommandHandler> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(BuySkinCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FindAsync(request.UserId);
            var skin = await _dbContext.Skins.FindAsync(request.SkinId);

            if (user != null && skin != null) 
            {         
                var skinCost = (double)skin.PriceWithDiscount;

                if (skinCost <= user.Balance) 
                {
                    user.Balance -= skinCost;

                    var entity = new SoldSkinsEntity()
                    {
                        User = user,
                        Skin = skin,
                        CreatedDate = DateTime.UtcNow
                    };

                    await _dbContext.SoldsSkins.AddAsync(entity, cancellationToken);

                    _logger.LogInformation("Skin purchased successfully.");

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    return true;
                }

                return false;
            }

            return false;
        }
    }
}