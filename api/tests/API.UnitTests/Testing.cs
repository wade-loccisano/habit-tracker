using Domain.Models;
using Infrastructure;
using Infrastructure.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace API.UnitTests;

[SetUpFixture]
public partial class Testing
{
    private static ITestDatabase _database = null!;
    private static HabitTrackerWebApplicationFactory _factory = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static string? _userId;

    [OneTimeSetUp]
    public async Task RunBeforeAnyTests()
    {
        _database = await TestDatabaseFactory.CreateAsync();

        _factory = new HabitTrackerWebApplicationFactory(_database.GetConnection(), _database.GetConnectionString());

        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public static async Task ResetState()
    {
        try
        {
            await _database.ResetAsync();
        }
        catch (Exception)
        {
        }
    }

    public static string GetTestConnectionString(IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("HabitTrackerDb") ?? string.Empty;
        return connectionString;
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using IServiceScope scope = _scopeFactory.CreateScope();

        HabitTrackerDbContext context = scope.ServiceProvider.GetRequiredService<HabitTrackerDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using IServiceScope scope = _scopeFactory.CreateScope();

        ISender mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    public static async Task SendAsync(IBaseRequest request)
    {
        using IServiceScope scope = _scopeFactory.CreateScope();

        ISender mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        await mediator.Send(request);
    }

    public static string? GetUserId() => _userId;

    public static async Task<string> RunAsDefaultUserAsync() 
        => await RunAsUserAsync("test@local", "Testing1234!", Array.Empty<string>());

    //public static async Task<string> RunAsAdministratorAsync() =>
    //    await RunAsUserAsync("administrator@local", "Administrator1234!", new[] { Roles.Administrator });

    public static async Task<string> RunAsUserAsync(string userName, string password, string[] roles)
    {
        using IServiceScope scope = _scopeFactory.CreateScope();

        UserManager<User> userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        var user = new User { UserName = userName, Email = userName };

        IdentityResult result = await userManager.CreateAsync(user, password);

        if (roles.Length != 0)
        {
            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (string role in roles)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }

            await userManager.AddToRolesAsync(user, roles);
        }

        if (result.Succeeded)
        {
            _userId = user.Id;

            return _userId;
        }

        string errors = string.Join(Environment.NewLine, result.ToApplicationResult().Errors);

        throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
    }

    [OneTimeTearDown]
    public async Task RunAfterAnyTests()
    {
        await _database.DisposeAsync();
        await _factory.DisposeAsync();
    }
}
