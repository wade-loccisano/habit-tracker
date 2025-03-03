using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IHostApplicationBuilder builder)
    {
        var connectionString = "Hi there"; // builder.Configuration.GetConnectionString("Db");
        builder.Services.AddHealthChecks();

        //services.AddDbContext<ApplicationDbContext>((sp, options) =>
        //{

        //});3
        Console.WriteLine("conn string is: ", connectionString);
    }
}
