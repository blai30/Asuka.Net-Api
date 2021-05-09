using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Application.Entities;
using AsukaApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.Tags
{
    public class Get
    {
        public sealed record Query(int? Id, string? Name, ulong? GuildId) : IRequest<IEnumerable<Tag>>;

        public sealed class QueryHandler : IRequestHandler<Query, IEnumerable<Tag>>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _factory;

            public QueryHandler(IDbContextFactory<ApplicationDbContext> factory)
            {
                _factory = factory;
            }

            public async Task<IEnumerable<Tag>> Handle(Query request, CancellationToken cancellationToken)
            {
                await using var context = _factory.CreateDbContext();

                var queryable = context.Tags
                    .AsQueryable();

                if (request.Id.HasValue)
                {
                    queryable = queryable
                        .Where(entity => entity.Id == request.Id);
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    queryable = queryable
                        .Where(entity => entity.Name == request.Name);
                }

                if (request.GuildId.HasValue)
                {
                    queryable = queryable
                        .Where(entity => entity.GuildId == request.GuildId);
                }

                var entities = await queryable
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);

                return entities;
            }
        }
    }
}
