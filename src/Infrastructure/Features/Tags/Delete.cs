using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.Tags
{
    public class Delete
    {
        public sealed record Command(int Id) : IRequest;

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

                var tag = await context.Tags
                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                context.Tags.Remove(tag);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
