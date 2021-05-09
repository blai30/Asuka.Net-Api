using System.Text.Json.Serialization;

namespace AsukaApi.Application.Entities
{
    public class ReactionRole
    {
        public int Id { get; set; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public ulong GuildId { get; set; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public ulong ChannelId { get; set; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public ulong MessageId { get; set; }

        [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
        public ulong RoleId { get; set; }

        public string Reaction { get; set; }
    }
}
