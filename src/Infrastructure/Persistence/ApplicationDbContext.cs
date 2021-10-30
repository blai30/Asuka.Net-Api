using System.Reflection;
using AsukaApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AsukaApi.Infrastructure.Persistence;

// TODO: Change DbContext to ApiAuthorizationDbContext<ApplicationUser> when adding authorization.
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
    {
    }

    public DbSet<Tag> Tags { get; set; } = default!;
    public DbSet<ReactionRole> ReactionRoles { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensure correct collation.
        modelBuilder.UseCollation("utf8mb4_unicode_ci");

        // Load entity type configuration mappers.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
