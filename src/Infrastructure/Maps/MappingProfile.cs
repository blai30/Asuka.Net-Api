using AsukaApi.Application.Entities;
using AsukaApi.Infrastructure.Features.ReactionRoles;
using AsukaApi.Infrastructure.Features.Tags;
using AutoMapper.Configuration;

namespace AsukaApi.Infrastructure.Maps
{
    public class MappingProfile : MapperConfigurationExpression
    {
        public MappingProfile()
        {
            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<ReactionRole, ReactionRoleDto>().ReverseMap();
        }
    }
}
