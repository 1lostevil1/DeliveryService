using DeliveryServiceDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DeliveryServiceWeb.Service.IoC;

public class DbContextConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();
        var connectionString = configuration.GetValue<string>("DeliveryServiceDbContext");

        builder.Services.AddDbContextFactory<DeliveryServiceDbContext>(
            options => { options.UseNpgsql(connectionString); },
            ServiceLifetime.Scoped);
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DeliveryServiceDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}