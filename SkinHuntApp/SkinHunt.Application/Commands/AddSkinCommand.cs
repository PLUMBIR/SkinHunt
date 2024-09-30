using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkinHunt.Application.Common.Entities;
using SkinHunt.Application.Common.Models;

namespace SkinHunt.Application.Commands
{
    public class AddSkinCommand : IRequest<SkinDto>
    {
        public SkinEntity Model { get; set; }

        public AddSkinCommand(SkinEntity model)
        {
            Model = model;
        }
    }

    public class AddSkinDbCommandHandler : IRequestHandler<AddSkinCommand, SkinDto>
    {
        private readonly DbContext _db;
        private readonly ILogger<AddSkinDbCommandHandler> _logger;
        private readonly IMapper _mapper;

        public AddSkinDbCommandHandler(
            DbContext db,
            ILogger<AddSkinDbCommandHandler> logger,
            IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<SkinDto> Handle(AddSkinCommand request, CancellationToken cancellationToken)
        {
            if (!_db.Skins.Any(x => x.Name.Equals(request.Model.Name)
                && x.Type.Equals(request.Model.Type)
                && x.Float.Equals(request.Model.Float)))
            {           
                await _db.Skins.AddAsync(request.Model, cancellationToken);

                _logger.LogInformation("Skin added successfully.");

                await _db.SaveChangesAsync(cancellationToken);
            }

            return await _db.Skins
                .ProjectTo<SkinDto>(_mapper.ConfigurationProvider)
                .FirstAsync(x => x.Name.Equals(request.Model.Name) 
                    && x.Type.Equals(request.Model.Type) 
                    && x.Float.Equals(request.Model.Float),
                    cancellationToken);
        }
    }
}
