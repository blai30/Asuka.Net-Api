using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Application.Entities;
using AsukaApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.ReactionRoles
{
    public sealed class Create
    {
        public sealed record Command(ulong GuildId, ulong ChannelId, ulong MessageId, ulong RoleId, string Reaction) : IRequest;

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

                var reactionRole = new ReactionRole
                {
                    GuildId = request.GuildId,
                    ChannelId = request.ChannelId,
                    MessageId = request.MessageId,
                    RoleId = request.RoleId,
                    Reaction = request.Reaction
                };

                await context.ReactionRoles.AddAsync(reactionRole, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
