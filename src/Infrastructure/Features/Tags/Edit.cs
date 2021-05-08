using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.Tags
{
    public sealed class Edit
    {
        public sealed record Command(int Id, string Content, string? Reaction) : IRequest;

        public sealed class CommandHandler : IRequestHandler<Command>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _factory;

            public CommandHandler(IDbContextFactory<ApplicationDbContext> factory)
            {
                _factory = factory;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await using var context = _factory.CreateDbContext();

                var entity = await context.Tags
                    .AsQueryable()
                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                entity.Content = request.Content;
                entity.Reaction = request.Reaction;

                context.Tags.Attach(entity);
                context.Entry(entity).Property(t => t.Content).IsModified = true;
                context.Entry(entity).Property(t => t.Reaction).IsModified = true;
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
