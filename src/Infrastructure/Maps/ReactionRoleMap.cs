using AsukaApi.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AsukaApi.Infrastructure.Maps
{
    /// <summary>
    ///     Build ReactionRole model for reaction roles table.
    /// </summary>
    public class ReactionRoleMap : IEntityTypeConfiguration<ReactionRole>
    {
        public void Configure(EntityTypeBuilder<ReactionRole> builder)
        {
            // Let database generate these values.
            builder.Property(e => e.CreatedAt).ValueGeneratedOnAdd();
            builder.Property(e => e.UpdatedAt).ValueGeneratedOnAddOrUpdate();
        }
    }
}
