namespace API.UnitTests;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new PostgreSQLTestcontainersTestDatabase();

        await database.InitializeAsync();

        return database;
    }
}
