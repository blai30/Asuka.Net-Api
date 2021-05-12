using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.Tags
{
    public static class Get
    {
        public sealed record Query(int Id) : IRequest<TagDto?>;

        public sealed class QueryHandler : IRequestHandler<Query, TagDto?>
        {
            private readonly IDbContextFactory<ApplicationDbContext> _factory;
            private readonly IMapper _mapper;

            public QueryHandler(IDbContextFactory<ApplicationDbContext> factory, IMapper mapper)
            {
                _factory = factory;
                _mapper = mapper;
            }

            public async Task<TagDto?> Handle(Query request, CancellationToken cancellationToken)
            {
                await using var context = _factory.CreateDbContext();
                var entity = context.Tags
                    .AsNoTracking()
                    .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

                var dto = _mapper.Map<TagDto>(entity);
                return dto;
            }
        }
    }
}
