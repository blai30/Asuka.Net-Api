using System.Text.Json.Serialization;
using AsukaApi.Application.Common;

namespace AsukaApi.Application.Entities
{
    public class Tag : AuditableEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Content { get; set; }

        public string? Reaction { get; set; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public ulong GuildId { get; set; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public ulong UserId { get; set; }

        public int UsageCount { get; set; }
    }
}
