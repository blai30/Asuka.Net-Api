using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Domain.Models;
using AsukaApi.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.Tags
{
    public static class Create
    {
        public sealed record Command(string Name, string Content, string? Reaction, ulong GuildId, ulong UserId) : IRequest<TagDto?>;

        public sealed class CommandHandler : IRequestHandler<Command, TagDto?>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _factory;
            private readonly IMapper _mapper;

            public CommandHandler(IDbContextFactory<ApplicationDbContext> factory, IMapper mapper)
            {
                _factory = factory;
                _mapper = mapper;
            }

            public async Task<TagDto?> Handle(Command request, CancellationToken cancellationToken)
            {
                var dto = new TagDto
                {
                    Name = request.Name,
                    Content = request.Content,
                    Reaction = request.Reaction,
                    GuildId = request.GuildId,
                    UserId = request.UserId
                };

                var entity = _mapper.Map<Tag>(dto);

                await using var context = _factory.CreateDbContext();
                await context.Tags.AddAsync(entity, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return dto;
            }
        }
    }
}
