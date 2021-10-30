using AsukaApi.Domain.Common;

namespace AsukaApi.Domain.Models;

public class Tag : AuditableEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public string Content { get; set; } = default!;

    public string? Reaction { get; set; }

    public ulong GuildId { get; set; }

    public ulong UserId { get; set; }

    public int UsageCount { get; set; }
}
