using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Application.Entities;
using AsukaApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.Tags
{
    public static class Get
    {
        public sealed record Query(int Id) : IRequest<Tag>;

        public sealed class QueryHandler : IRequestHandler<Query, Tag>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _factory;

            public QueryHandler(IDbContextFactory<ApplicationDbContext> factory)
            {
                _factory = factory;
            }

            public async Task<Tag> Handle(Query request, CancellationToken cancellationToken)
            {
                await using var context = _factory.CreateDbContext();

                var entity = await context.Tags
                    .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

                return entity;
            }
        }
    }
}
