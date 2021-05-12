using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Application.Entities;
using AsukaApi.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.Tags
{
    public static class Create
    {
        public sealed record Command(string Name, string Content, string? Reaction, ulong GuildId, ulong UserId) : IRequest;

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

                var dto = new TagDto
                {
                    Name = request.Name,
                    Content = request.Content,
                    Reaction = request.Reaction,
                    GuildId = request.GuildId,
                    UserId = request.UserId
                };

                var entity = _mapper.Map<Tag>(dto);

                await context.Tags.AddAsync(entity, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
