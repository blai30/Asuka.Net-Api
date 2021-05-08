using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Application.Entities;
using AsukaApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.Tags
{
    public sealed class Create
    {
        public sealed record Command(string Name, string Content, string? Reaction, ulong GuildId, ulong UserId) : IRequest;

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

                var tag = new Tag
                {
                    Name = request.Name,
                    Content = request.Content,
                    Reaction = request.Reaction,
                    GuildId = request.GuildId,
                    UserId = request.UserId
                };

                await context.Tags.AddAsync(tag, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
