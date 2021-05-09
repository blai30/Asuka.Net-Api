using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.ReactionRoles
{
    public class DeleteBulk
    {
        public sealed record Command(ulong? MessageId, ulong? RoleId) : IRequest;

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

                var entities = await context.ReactionRoles
                    .AsNoTracking()
                    .Where(entity =>
                        entity.MessageId == request.MessageId &&
                        entity.RoleId == request.RoleId)
                    .ToListAsync(cancellationToken);

                if (entities is null || entities.Count <= 0)
                {
                    throw new HttpRequestException("Entities not found", null, HttpStatusCode.NotFound);
                }

                context.ReactionRoles.RemoveRange(entities);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
