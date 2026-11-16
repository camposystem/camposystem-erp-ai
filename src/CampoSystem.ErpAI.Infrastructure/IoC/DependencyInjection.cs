using CampoSystem.ErpAI.Application.Identity.Authorization;
using CampoSystem.ErpAI.Application.Products.Repositories;
using CampoSystem.ErpAI.Infrastructure.Identity.Authorization;
using CampoSystem.ErpAI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CampoSystem.ErpAI.Infrastructure.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
           configuration.GetConnectionString("PostgreSQL")
           ?? throw new InvalidOperationException(
               "Connection string 'PostgreSQL' não configurada.");

        services.AddDbContext<ProductDbContext>(options =>
            options.UseNpgsql(connectionString,
        npgsql => npgsql.MigrationsHistoryTable(
            "__EFMigrationsHistory",
            "products")));

        services.AddDbContext<IdentityDbContext>(options =>
            options.UseNpgsql(connectionString,
        npgsql => npgsql.MigrationsHistoryTable(
            "__EFMigrationsHistory",
            "identity")));

        services
            .AddScoped<IProductRepository, ProductRepository>();

        services
            .AddScoped<IAuthorizationUserService, AuthorizationUserService>();

        return services;
    }
}