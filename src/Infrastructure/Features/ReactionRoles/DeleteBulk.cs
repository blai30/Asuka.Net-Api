using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Domain.Models;
using AsukaApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.ReactionRoles
{
    public static class DeleteBulk
    {
        public sealed record Command(ulong? MessageId, ulong? RoleId, string? Reaction) : IRequest;

        public sealed class CommandHandler : IRequestHandler<Command>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _factory;

            public CommandHandler(IDbContextFactory<ApplicationDbContext> factory)
            {
                _factory = factory;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                List<ReactionRole>? entities = null;
                await using var context = _factory.CreateDbContext();

                if (request.MessageId.HasValue && request.RoleId.HasValue)
                {
                    entities = await context.ReactionRoles
                        .AsNoTracking()
                        .Where(entity =>
                            entity.MessageId == request.MessageId &&
                            entity.RoleId == request.RoleId)
                        .ToListAsync(cancellationToken);
                }
                else if (request.MessageId.HasValue && !string.IsNullOrWhiteSpace(request.Reaction))
                {
                    entities = await context.ReactionRoles
                        .AsNoTracking()
                        .Where(entity =>
                            entity.MessageId == request.MessageId &&
                            entity.Reaction == request.Reaction)
                        .ToListAsync(cancellationToken);
                }
                else if (request.MessageId.HasValue)
                {
                    entities = await context.ReactionRoles
                        .AsNoTracking()
                        .Where(entity =>
                            entity.MessageId == request.MessageId)
                        .ToListAsync(cancellationToken);
                }
                else if (request.RoleId.HasValue)
                {
                    entities = await context.ReactionRoles
                        .AsNoTracking()
                        .Where(entity =>
                            entity.RoleId == request.RoleId)
                        .ToListAsync(cancellationToken);
                }

                if (entities is null || entities.Count <= 0)
                {
                    return Unit.Value;
                }

                context.ReactionRoles.RemoveRange(entities);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
