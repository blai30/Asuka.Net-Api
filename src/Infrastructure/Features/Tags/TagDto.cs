using System.Text.Json.Serialization;

namespace AsukaApi.Infrastructure.Features.Tags
{
    public record TagDto
    {
        public int Id { get; init; }

        public string Name { get; init; } = default!;

        public string Content { get; init; } = default!;

        public string? Reaction { get; init; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public ulong GuildId { get; init; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public ulong UserId { get; init; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public int UsageCount { get; init; }
    }
}
