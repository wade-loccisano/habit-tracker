using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        Console.WriteLine("Ass is: ", assembly);

        //services.AddMediatr

        return services;
    }
}
