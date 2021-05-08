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
    public class Get
    {
        public sealed record Query(ulong? GuildId, ulong? ChannelId, ulong? MessageId, ulong? RoleId, string? Reaction) : IRequest<IEnumerable<ReactionRole>>;

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

                if (request.GuildId.HasValue)
                {
                    queryable = queryable.Where(reactionRole => reactionRole.GuildId == request.GuildId);
                }

                if (request.ChannelId.HasValue)
                {
                    queryable = queryable.Where(reactionRole => reactionRole.ChannelId == request.ChannelId);
                }

                if (request.MessageId.HasValue)
                {
                    queryable = queryable.Where(reactionRole => reactionRole.MessageId == request.MessageId);
                }

                if (request.RoleId.HasValue)
                {
                    queryable = queryable.Where(reactionRole => reactionRole.RoleId == request.RoleId);
                }

                if (!string.IsNullOrWhiteSpace(request.Reaction))
                {
                    queryable = queryable.Where(reactionRole => reactionRole.Reaction == request.Reaction);
                }

                var reactionRoles = await queryable
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return reactionRoles;
            }
        }
    }
}
