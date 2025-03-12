using System.Data.Common;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Respawn;

namespace API.UnitTests;
public class PostgreSQLTestDatabase : ITestDatabase
{
    private readonly string _connectionString = null!;
    private NpgsqlConnection _connection = null!;
    private Respawner _respawner = null!;

    public PostgreSQLTestDatabase()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        string? connectionString = configuration.GetConnectionString("HabitTrackerDb");

        //Guard.Against.Null(connectionString);

        _connectionString = connectionString!;
    }

    public async Task InitializeAsync()
    {
        _connection = new NpgsqlConnection(_connectionString);

        DbContextOptions<HabitTrackerDbContext> options = new DbContextOptionsBuilder<HabitTrackerDbContext>()
            .UseNpgsql(_connectionString)
            .ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning))
            .Options;

        var context = new HabitTrackerDbContext(options);

        context.Database.EnsureDeleted();
        //context.Database.Migrate();

        await _connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            TablesToIgnore = ["__EFMigrationsHistory"]
        });
        await _connection.CloseAsync();
    }

    public DbConnection GetConnection() => _connection;

    public string GetConnectionString() => null!;

    public async Task ResetAsync()
    {
        await _connection.OpenAsync();
        await _respawner.ResetAsync(_connection);
        await _connection.CloseAsync();
    }

    public async Task DisposeAsync() => await _connection.DisposeAsync();
}
