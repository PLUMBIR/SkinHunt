using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkinHunt.Application.Commands
{
    public class RemoveSkinFromBasketCommand : IRequest
    {
        public Guid Id { get; set; }

        public RemoveSkinFromBasketCommand(Guid id)
        {
            Id = id;
        }
    }

    public class RemoveSkinFromBasketCommandHandler : IRequestHandler<RemoveSkinFromBasketCommand>
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<RemoveSkinFromBasketCommandHandler> _logger;

        public RemoveSkinFromBasketCommandHandler(DbContext dbContext, ILogger<RemoveSkinFromBasketCommandHandler> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task Handle(RemoveSkinFromBasketCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Basket.FirstAsync(o => o.Id == request.Id);
            
            _dbContext.Basket.Remove(entity);

            _logger.LogInformation("Skin removed from basket successfully.");

            await _dbContext.SaveChangesAsync(cancellationToken);    
        }
    }
}
