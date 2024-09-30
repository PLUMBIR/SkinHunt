using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SkinHunt.Application.Common.Entities;
using SkinHunt.Application.Common.Models;

namespace SkinHunt.Application.Commands
{
    public class InitSkinsCommand : IRequest
    {
    }

    public class InitSkinsCommandHandler : IRequestHandler<InitSkinsCommand>
    {
        private readonly DbContext _db;
        private readonly ILogger<InitSkinsCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly string SkinsFileName = "Skins.json";

        public InitSkinsCommandHandler(DbContext db, ILogger<InitSkinsCommandHandler> logger, 
            IMediator mediator, IMapper mapper)
        {
            _db = db;
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task Handle(InitSkinsCommand request, CancellationToken cancellationToken)
        {
            await _db.Database.EnsureCreatedAsync(cancellationToken);

            if (!_db.Skins.Any())
            {
                try
                {
                    using var reader = new StreamReader(SkinsFileName);

                    var json = reader.ReadToEnd();

                    var skins = JsonConvert.DeserializeObject<List<SkinEntity>>(json);

                    if (skins is not null)
                    {
                        foreach (var skin in skins)
                        {
                            var type = _mapper.Map<ItemTypeModel>(skin.Type);

                            await _mediator.Send(new AddItemTypeCommand(type), cancellationToken);

                            var typeEntity = await _db.SkinTypes
                                .FirstAsync(o => o.Category == type.Category && o.Subcategory == type.Subcategory);

                            skin.Type = typeEntity;                    

                            await _mediator.Send(new AddSkinCommand(skin), cancellationToken);
                        }            
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Init database by skins failed. Message: {ex.Message}");
                }      
            }
        }
    }
}
