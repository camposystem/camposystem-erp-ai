using Npgsql;
using Respawn;

namespace CampoSystem.ErpAI.IntegrationTests.Infrastructure;

public class DatabaseFixture : IAsyncLifetime
{
    public  string ConnectionString =
        "Host=localhost;Port=5432;Database=camposystem;Username=postgres;Password=postgres123";


    public Respawner DatabaseRespawner { get; private set; } = default!;


    public async Task InitializeAsync()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);

        await connection.OpenAsync();

        DatabaseRespawner = await Respawner.CreateAsync(connection);
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