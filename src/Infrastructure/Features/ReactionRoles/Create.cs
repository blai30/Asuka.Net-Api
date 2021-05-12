using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Application.Entities;
using AsukaApi.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.ReactionRoles
{
    public static class Create
    {
        public sealed record Command(ulong GuildId, ulong ChannelId, ulong MessageId, ulong RoleId, string Reaction) : IRequest;

        public sealed class CommandHandler : IRequestHandler<Command>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _factory;
            private readonly IMapper _mapper;

            public CommandHandler(IDbContextFactory<ApplicationDbContext> factory, IMapper mapper)
            {
                _factory = factory;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await using var context = _factory.CreateDbContext();

                var dto = new ReactionRoleDto
                {
                    GuildId = request.GuildId,
                    ChannelId = request.ChannelId,
                    MessageId = request.MessageId,
                    RoleId = request.RoleId,
                    Reaction = request.Reaction
                };

                var entity = _mapper.Map<ReactionRole>(dto);

                await context.ReactionRoles.AddAsync(entity, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
