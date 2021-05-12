using AsukaApi.Domain.Common;

namespace AsukaApi.Domain.Models
{
    public class ReactionRole : AuditableEntity
    {
        public int Id { get; set; }

        public ulong GuildId { get; set; }

        public ulong ChannelId { get; set; }

        public ulong MessageId { get; set; }

        public ulong RoleId { get; set; }

        public string Reaction { get; set; } = default!;
    }
}
