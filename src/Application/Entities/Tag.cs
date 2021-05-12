using AsukaApi.Application.Common;

namespace AsukaApi.Application.Entities
{
    public class Tag : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string? Reaction { get; set; }

        public ulong GuildId { get; set; }

        public ulong UserId { get; set; }

        public int UsageCount { get; set; }
    }
}
