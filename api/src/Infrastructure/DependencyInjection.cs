using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IHostApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks();

        builder.Services.AddDbContext<HabitTrackerDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnection")));
    }
}
