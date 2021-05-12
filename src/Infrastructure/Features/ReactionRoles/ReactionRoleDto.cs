using System.Text.Json.Serialization;

namespace AsukaApi.Infrastructure.Features.ReactionRoles
{
    public record ReactionRoleDto
    {
        public int Id { get; init; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public ulong GuildId { get; init; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public ulong ChannelId { get; init; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public ulong MessageId { get; init; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public ulong RoleId { get; init; }

        public string Reaction { get; init; } = default!;
    }
}
