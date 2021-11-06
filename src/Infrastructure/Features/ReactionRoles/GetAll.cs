using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.ReactionRoles;

public static class GetAll
{
    public sealed record Query(ulong? GuildId, ulong? ChannelId, ulong? MessageId, ulong? RoleId) : IRequest<IEnumerable<ReactionRoleDto>?>;

    public sealed class QueryHandler : IRequestHandler<Query, IEnumerable<ReactionRoleDto>?>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;
        private readonly IMapper _mapper;

        public QueryHandler(IDbContextFactory<ApplicationDbContext> factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReactionRoleDto>?> Handle(Query request, CancellationToken cancellationToken)
        {
            await using var context = await _factory.CreateDbContextAsync(cancellationToken);
            var queryable = context.ReactionRoles.AsQueryable();
            var entities = _mapper.ProjectTo<ReactionRoleDto>(queryable);

            if (request.GuildId.HasValue)
            {
                entities = entities
                    .Where(entity => entity.GuildId == request.GuildId);
            }

            if (request.ChannelId.HasValue)
            {
                entities = entities
                    .Where(entity => entity.ChannelId == request.ChannelId);
            }

            if (request.MessageId.HasValue)
            {
                entities = entities
                    .Where(entity => entity.MessageId == request.MessageId);
            }

            if (request.RoleId.HasValue)
            {
                entities = entities
                    .Where(entity => entity.RoleId == request.RoleId);
            }

            var dto = await entities
                .ToListAsync(cancellationToken);

            return dto;
        }
    }
}
