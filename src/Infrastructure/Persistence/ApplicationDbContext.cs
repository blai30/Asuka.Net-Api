using System.Reflection;
using AsukaApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AsukaApi.Infrastructure.Persistence;

// TODO: Change DbContext to ApiAuthorizationDbContext<ApplicationUser> when adding authorization.
public class ApplicationDbContext : DbContext
{
    private readonly IServiceScopeFactory _scopeFactory;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IServiceScopeFactory scopeFactory) :
        base(options)
    {
        _scopeFactory = scopeFactory;
    }

    public DbSet<Tag> Tags { get; set; } = default!;
    public DbSet<ReactionRole> ReactionRoles { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        using var scope = _scopeFactory.CreateScope();
        string connectionString = scope.ServiceProvider
            .GetRequiredService<IConfiguration>()
            .GetConnectionString("Docker");

        optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        // Map PascalCase POCO properties to snake_case MySQL tables and columns.
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensure correct collation.
        modelBuilder.UseCollation("utf8mb4_unicode_ci");

        // Load entity type configuration mappers.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
