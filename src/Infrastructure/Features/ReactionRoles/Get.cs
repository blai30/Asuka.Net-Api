using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.ReactionRoles;

public static class Get
{
    public sealed record Query(int Id) : IRequest<ReactionRoleDto?>;

    public sealed class QueryHandler : IRequestHandler<Query, ReactionRoleDto?>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;
        private readonly IMapper _mapper;

        public QueryHandler(IDbContextFactory<ApplicationDbContext> factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public async Task<ReactionRoleDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            await using var context = await _factory.CreateDbContextAsync(cancellationToken);
            var entity = await context.ReactionRoles
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            var dto = _mapper.Map<ReactionRoleDto>(entity);
            return dto;
        }
    }
}
