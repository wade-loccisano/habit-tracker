using System.Data.Common;

namespace API.UnitTests;

public interface ITestDatabase
{
    Task InitializeAsync();
    DbConnection GetConnection();
    string GetConnectionString();
    Task ResetAsync();
    Task DisposeAsync();
}
