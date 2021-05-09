using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Application.Entities;
using AsukaApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.ReactionRoles
{
    public static class Get
    {
        public sealed record Query(int Id) : IRequest<ReactionRole>;

        public sealed class QueryHandler : IRequestHandler<Query, ReactionRole>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _factory;

            public QueryHandler(IDbContextFactory<ApplicationDbContext> factory)
            {
                _factory = factory;
            }

            public async Task<ReactionRole> Handle(Query request, CancellationToken cancellationToken)
            {
                await using var context = _factory.CreateDbContext();

                var entity = await context.ReactionRoles
                    .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

                return entity;
            }
        }
    }
}
