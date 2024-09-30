using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SkinHunt.Application.Common.Models;

namespace SkinHunt.Application.Queries
{
    public class GetSkinByIdQuery : IRequest<SkinDto>
    {
        public Guid Id { get; set; }

        public GetSkinByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetSkinByIdQueryHandler : IRequestHandler<GetSkinByIdQuery, SkinDto>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public GetSkinByIdQueryHandler(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SkinDto> Handle(GetSkinByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Skins
                .ProjectTo<SkinDto>(_mapper.ConfigurationProvider)
                .FirstAsync(x => x.Id == request.Id.ToString());
        }
    }
}
