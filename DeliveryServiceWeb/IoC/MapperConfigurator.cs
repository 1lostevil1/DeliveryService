using DeliveryServiceDL.Mapper;
using DeliveryServiceWeb.Mapper;

namespace DeliveryServiceWeb.Service.IoC;

public static class MapperConfigurator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(config =>
        {
            config.AddProfile<AuthBLProfile>();
            config.AddProfile<AuthServiceProfile>();
            
            config.AddProfile<UsersBLProfile>();
            config.AddProfile<UsersServiceProfile>();
            
            config.AddProfile<PermissionsBLProfile>();
            config.AddProfile<PermissionsServiceProfile>();
        });
    }
}