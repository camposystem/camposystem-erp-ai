using CampoSystem.ErpAI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;

namespace CampoSystem.ErpAI.IntegrationTests.Infrastructure;

public class DatabaseFixture : IAsyncLifetime
{
    public string ConnectionString { get; } =
    Environment.GetEnvironmentVariable("CAMPOSYSTEM_TEST_CONNECTION")
    ?? throw new InvalidOperationException(
        "Variável CAMPOSYSTEM_TEST_CONNECTION não configurada.");

    public Respawner DatabaseRespawner { get; private set; } = default!;


    public async Task InitializeAsync()
    {

        var options = new DbContextOptionsBuilder<ProductDbContext>()
            .UseNpgsql(
                ConnectionString,
                npgsql => npgsql.MigrationsHistoryTable(
                    "__EFMigrationsHistory",
                    "products"))
            .Options;

        var identityOptions = new DbContextOptionsBuilder<IdentityDbContext>()
            .UseNpgsql(
                ConnectionString,
                npgsql => npgsql.MigrationsHistoryTable(
                    "__EFMigrationsHistory",
                    "identity"))
            .Options;


        await using var context = new ProductDbContext(options);

        await context.Database.MigrateAsync();

        await using var identityContext = new IdentityDbContext(identityOptions);

        await identityContext.Database.MigrateAsync();

        await using var connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync();

        DatabaseRespawner = await Respawner.CreateAsync(
    connection,
    new RespawnerOptions
    {
        SchemasToInclude = ["identity", "products"],
        TablesToIgnore =
        [
            "__EFMigrationsHistory"
        ],
        DbAdapter = DbAdapter.Postgres
    });
    }

    public async Task ResetDatabaseAsync()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);

        await connection.OpenAsync();

        await DatabaseRespawner.ResetAsync(connection);
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

}