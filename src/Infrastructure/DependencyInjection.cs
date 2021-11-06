using System.Reflection;
using AsukaApi.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AsukaApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddDbContextFactory<ApplicationDbContext>(builder =>
        {
            // Default to AsNoTracking(), use AsTracking() otherwise.
            builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            // Map PascalCase POCO properties to snake_case tables and columns.
            builder.UseSnakeCaseNamingConvention();
        });

        return services;
    }
}
