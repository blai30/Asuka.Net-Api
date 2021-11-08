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
        modelBuilder.HasDefaultSchema("asuka_net");
        // Load entity type configuration mappers.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
