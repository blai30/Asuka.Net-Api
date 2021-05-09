using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Application.Entities;
using AsukaApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.ReactionRoles
{
    public static class GetAll
    {
        public sealed record Query(int? Id, ulong? GuildId, ulong? ChannelId, ulong? MessageId, ulong? RoleId, string? Reaction) : IRequest<IEnumerable<ReactionRole>>;

        public sealed class QueryHandler : IRequestHandler<Query, IEnumerable<ReactionRole>>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _factory;

            public QueryHandler(IDbContextFactory<ApplicationDbContext> factory)
            {
                _factory = factory;
            }

            public async Task<IEnumerable<ReactionRole>> Handle(Query request, CancellationToken cancellationToken)
            {
                await using var context = _factory.CreateDbContext();

                var queryable = context.ReactionRoles
                    .AsQueryable();

                if (request.Id.HasValue)
                {
                    queryable = queryable
                        .Where(entity => entity.Id == request.Id);
                }

                if (request.GuildId.HasValue)
                {
                    queryable = queryable
                        .Where(entity => entity.GuildId == request.GuildId);
                }

                if (request.ChannelId.HasValue)
                {
                    queryable = queryable
                        .Where(entity => entity.ChannelId == request.ChannelId);
                }

                if (request.MessageId.HasValue)
                {
                    queryable = queryable
                        .Where(entity => entity.MessageId == request.MessageId);
                }

                if (request.RoleId.HasValue)
                {
                    queryable = queryable
                        .Where(entity => entity.RoleId == request.RoleId);
                }

                if (!string.IsNullOrWhiteSpace(request.Reaction))
                {
                    queryable = queryable
                        .Where(entity => entity.Reaction == request.Reaction);
                }

                var entities = await queryable
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return entities;
            }
        }
    }
}
