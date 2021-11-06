using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.Tags;

public static class GetAll
{
    public sealed record Query(string? Name, ulong? GuildId) : IRequest<IEnumerable<TagDto>?>;

    public sealed class QueryHandler : IRequestHandler<Query, IEnumerable<TagDto>?>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;
        private readonly IMapper _mapper;

        public QueryHandler(IDbContextFactory<ApplicationDbContext> factory, IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagDto>?> Handle(Query request, CancellationToken cancellationToken)
        {
            await using var context = await _factory.CreateDbContextAsync(cancellationToken);
            var queryable = context.Tags.AsQueryable();
            var entities = _mapper.ProjectTo<TagDto>(queryable);

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                entities = entities
                    .Where(entity => entity.Name == request.Name);
            }

            if (request.GuildId.HasValue)
            {
                entities = entities
                    .Where(entity => entity.GuildId == request.GuildId);
            }

            var dto = await entities
                .ToListAsync(cancellationToken);

            return dto;
        }
    }
}
