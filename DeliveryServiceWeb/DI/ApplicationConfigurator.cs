using DeliveryServiceWeb.Service.IoC;

namespace DeliveryServiceWeb.Service.DI;

public static class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder, Settings.Settings settings)
    {
        SerilogConfigurator.ConfigureServices(builder);
        DbContextConfigurator.ConfigureServices(builder.Services, settings);
        AuthorizationConfigurator.ConfigureServices(builder.Services, settings);
        SwaggerConfigurator.ConfigureServices(builder.Services);
        MapperConfigurator.ConfigureServices(builder.Services);
        ServicesConfigurator.ConfigureServices(builder.Services, settings);

        builder.Services.AddControllers();
    }

    public static async Task ConfigureApplication(WebApplication app, Settings.Settings settings)
    {
        SerilogConfigurator.ConfigureApplication(app);
        DbContextConfigurator.ConfigureApplication(app);
        AuthorizationConfigurator.ConfigureApplication(app);
        await RepositoryInitializer.ConfigureApplication(app, settings);
        SwaggerConfigurator.ConfigureApplication(app);

        app.MapControllers();
    }
}