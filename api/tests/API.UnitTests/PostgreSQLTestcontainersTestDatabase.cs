using System.Data.Common;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using Respawn;
using Testcontainers.PostgreSql;

namespace API.UnitTests;

public class PostgreSQLTestcontainersTestDatabase : ITestDatabase
{
    private const string DefaultDatabase = "HabitTrackerTestDatabase";
    private readonly PostgreSqlContainer _container;
    private DbConnection _connection = null!;
    private string _connectionString = null!;
    private Respawner _respawner = null!;

    public PostgreSQLTestcontainersTestDatabase() 
        => _container = new PostgreSqlBuilder()
            .WithAutoRemove(true)
            .Build();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        await _container.ExecScriptAsync($"CREATE DATABASE {DefaultDatabase}");

        var builder = new NpgsqlConnectionStringBuilder(_container.GetConnectionString())
        {
            Database = DefaultDatabase
        };

        _connectionString = builder.ConnectionString;

        _connection = new NpgsqlConnection(_connectionString);

        DbContextOptions<HabitTrackerDbContext> options = new DbContextOptionsBuilder<HabitTrackerDbContext>()
            .UseNpgsql(_connectionString)
            .ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning))
            .Options;

        var context = new HabitTrackerDbContext(options);

        await context.Database.MigrateAsync();

        await _connection.OpenAsync();
        _respawner = await Respawner.CreateAsync(_connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            TablesToIgnore = ["__EFMigrationsHistory"]
        });
        await _connection.CloseAsync();
    }

    public DbConnection GetConnection()
    {
        string x = "x";
        Console.WriteLine(x);
        return _connection;
    }

    public string GetConnectionString()
    {
        string x = "x";
        Console.WriteLine(x);
        return _connectionString;
    }

    public async Task ResetAsync()
    {
        await _connection.OpenAsync();
        await _respawner.ResetAsync(_connection);
        await _connection.CloseAsync();
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _container.DisposeAsync();
    }
}
