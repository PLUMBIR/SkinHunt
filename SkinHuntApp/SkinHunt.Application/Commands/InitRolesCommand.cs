using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SkinHunt.Domain.Constants;

namespace SkinHunt.Application.Commands
{
    public class InitRolesCommand : IRequest
    {
    }

    public class InitRolesCommandHandler : IRequestHandler<InitRolesCommand>
    {
        private readonly DbContext _db;
        private readonly ILogger<InitRolesCommandHandler> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InitRolesCommandHandler(DbContext db, ILogger<InitRolesCommandHandler> logger, 
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _logger = logger;
            _roleManager = roleManager;
        }

        public async Task Handle(InitRolesCommand request, CancellationToken cancellationToken)
        {
            if (!_db.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole(RolesConstants.Admin));
                await _roleManager.CreateAsync(new IdentityRole(RolesConstants.User));

                _logger.LogInformation("Added default roles to database.");
            }
        }
    }
}
