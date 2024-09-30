using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using SkinHunt.Application.Common.Entities;
using SkinHunt.Application.Common.Models;

namespace SkinHunt.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SkinModel, SkinEntity>()
                .ReverseMap();

            CreateMap<ItemTypeModel, ItemTypeEntity>()
                .ReverseMap();

            CreateMap<UserEntity, UserDto>();
            
            CreateMap<SkinEntity, SkinDto>();
            
            CreateMap<BasketEntity, BasketDto>();
            
            CreateMap<ItemTypeEntity, ItemTypeDto>();

            CreateMap<SoldSkinsEntity, TransactionsDto>();
        }
    }
}
