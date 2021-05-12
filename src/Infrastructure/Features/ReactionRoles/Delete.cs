using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.ReactionRoles
{
    public static class Delete
    {
        public sealed record Command(int Id) : IRequest<ReactionRoleDto?>;

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
                await using var context = _factory.CreateDbContext();
                var entity = await context.ReactionRoles
                    .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                if (entity is null)
                {
                    throw new HttpRequestException("Entity not found", null, HttpStatusCode.NotFound);
                }

                context.ReactionRoles.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);

                var dto = _mapper.Map<ReactionRoleDto>(entity);
                return dto;
            }
        }
    }
}
