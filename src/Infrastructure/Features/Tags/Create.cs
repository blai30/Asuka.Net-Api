﻿using System.Threading;
using System.Threading.Tasks;
using AsukaApi.Domain.Models;
using AsukaApi.Infrastructure.Persistence;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Features.Tags;

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
            var entity = new Tag
            {
                Name = request.Name,
                Content = request.Content,
                Reaction = request.Reaction,
                GuildId = request.GuildId,
                UserId = request.UserId
            };

            await using var context = await _factory.CreateDbContextAsync(cancellationToken);
            var entry = await context.Tags.AddAsync(entity, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<TagDto>(entry.Entity);

            return dto;
        }
    }
}
