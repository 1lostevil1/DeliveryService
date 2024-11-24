using DeliveryServiceWeb.Service.IoC;

namespace DeliveryServiceWeb.Service.DI;

public static class ApplicationConfigurator
{
    public static void ConfigureServices(WebApplicationBuilder builder, Settings.Settings settings)
    {
        DbContextConfigurator.ConfigureServices(builder.Services, settings);
        SerilogConfigurator.ConfigureServices(builder);
        SwaggerConfigurator.ConfigureServices(builder.Services);
        MapperConfigurator.ConfigureServices(builder.Services);
        ServicesConfigurator.ConfigureServices(builder.Services, settings);

        builder.Services.AddControllers();
    }

    public static void ConfigureApplication(WebApplication app)
    {
        SerilogConfigurator.ConfigureApplication(app);
        SwaggerConfigurator.ConfigureApplication(app);
        DbContextConfigurator.ConfigureApplication(app);

        app.MapControllers();
    }
}