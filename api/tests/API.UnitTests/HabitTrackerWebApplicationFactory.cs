using Microsoft.AspNetCore.Hosting;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace API.UnitTests;

public class HabitTrackerWebApplicationFactory(
    DbConnection connection, 
    string connectionString) 
        : WebApplicationFactory<Program>
{
    private readonly DbConnection _connection = connection;
    private readonly string _connectionString = connectionString;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseSetting("ConnectionStrings:HabitTrackerDb", _connectionString);
        builder.ConfigureTestServices(services => services
                .RemoveAll<DbContextOptions<HabitTrackerDbContext>>()
                .AddDbContext<HabitTrackerDbContext>((sp, options) => options.UseNpgsql(_connection)));
    }
}
