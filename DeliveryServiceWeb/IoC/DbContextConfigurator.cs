using DeliveryServiceDataAccess;
using Microsoft.EntityFrameworkCore;

namespace DeliveryServiceWeb.Service.IoC;

public class DbContextConfigurator
{
    public static void ConfigureServices(IServiceCollection services, Settings.Settings settings)
    {
        var connectionString = settings.DbContextConnectionString;
        services.AddDbContextFactory<DbContext>(
            options => { options.UseNpgsql(connectionString); },
            ServiceLifetime.Scoped);
    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}