using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Domain.Models;
using AsukaApi.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.ReactionRoles
{
    public static class Create
    {
        public sealed record Command(ulong GuildId, ulong ChannelId, ulong MessageId, ulong RoleId, string Reaction) : IRequest<ReactionRoleDto?>;

        public sealed class CommandHandler : IRequestHandler<Command, ReactionRoleDto?>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _factory;
            private readonly IMapper _mapper;

            public CommandHandler(IDbContextFactory<ApplicationDbContext> factory, IMapper mapper)
            {
                _factory = factory;
                _mapper = mapper;
            }

            public async Task<ReactionRoleDto?> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new ReactionRole
                {
                    GuildId = request.GuildId,
                    ChannelId = request.ChannelId,
                    MessageId = request.MessageId,
                    RoleId = request.RoleId,
                    Reaction = request.Reaction
                };

                await using var context = _factory.CreateDbContext();
                var entry = await context.ReactionRoles.AddAsync(entity, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<ReactionRoleDto>(entry.Entity);

                return dto;
            }
        }
    }
}
