using DeliveryServiceDL.Mapper;
using DeliveryServiceWeb.Mapper;

namespace DeliveryServiceWeb.Service.IoC;

public static class MapperConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<UsersBLProfile>();
            config.AddProfile<UsersServiceProfile>();
        });
    }
}