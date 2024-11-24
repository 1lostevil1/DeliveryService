using AutoMapper;
using DeliveryServiceBL.User.Provider;
using DeliveryServiceDataAccess.Entities;
using DeliveryServiceDL.User.Manager;
using Microsoft.EntityFrameworkCore;

namespace DeliveryServiceWeb.Service.IoC;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services, Settings.Settings settings)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRepository<User>>(x => 
            new Repository<User>(x.GetRequiredService<IDbContextFactory<DbContext>>()));
        services.AddScoped<IUsersProvider>(x => 
            new UsersProvider(x.GetRequiredService<IRepository<User>>(), 
                x.GetRequiredService<IMapper>()));
        services.AddScoped<IUsersManager>(x =>
            new UsersManager(x.GetRequiredService<IRepository<User>>(),
                x.GetRequiredService<IMapper>()));
    }
}