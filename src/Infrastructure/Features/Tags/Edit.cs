using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.Tags
{
    public static class Edit
    {
        public sealed record Command(int Id, string Content, string? Reaction) : IRequest<TagDto?>;

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
                await using var context = _factory.CreateDbContext();
                var entity = await context.Tags
                    .AsNoTracking()
                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                if (entity is null)
                {
                    throw new HttpRequestException("Tag not found", null, HttpStatusCode.NotFound);
                }

                entity.Content = request.Content;
                entity.Reaction = request.Reaction;

                context.Tags.Attach(entity);
                context.Entry(entity).Property(t => t.Content).IsModified = true;
                context.Entry(entity).Property(t => t.Reaction).IsModified = true;
                await context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<TagDto>(entity);
                return dto;
            }
        }
    }
}
