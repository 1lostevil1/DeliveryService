
using AutoMapper;
using DeliveryServiceBL.Auth;
using DeliveryServiceBL.Permissions.Provider;
using DeliveryServiceBL.User.Provider;
using DeliveryServiceDataAccess;
using DeliveryServiceDataAccess.Entities;
using DeliveryServiceDL.User.Manager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeliveryServiceWeb.Service.IoC;

public static class ServicesConfigurator
{
    public static void ConfigureServices(IServiceCollection services, Settings.Settings settings)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IRepository<PermissionEntity>>(x =>
            new Repository<PermissionEntity>(x.GetRequiredService<IDbContextFactory<DeliveryServiceDbContext>>()));

        services.AddScoped(typeof(IRepository<User>), typeof(UsersRepository));
        services.AddScoped<IRepository<User>>(x =>
            new UsersRepository(x.GetRequiredService<IDbContextFactory<DeliveryServiceDbContext>>()));

        services.AddScoped<IPermissionsProvider>(x =>
            new PermissionsProvider(x.GetRequiredService<IRepository<PermissionEntity>>(),
                x.GetRequiredService<IMapper>()));

        services.AddScoped<IUsersProvider>(x =>
            new UsersProvider(x.GetRequiredService<IRepository<User>>(),
                x.GetRequiredService<IMapper>()));
        services.AddScoped<IUsersManager>(x =>
            new UsersManager(x.GetRequiredService<IRepository<User>>(),
                x.GetRequiredService<IRepository<PermissionEntity>>(),
                x.GetRequiredService<IMapper>()));

        services.AddScoped<IAuthProvider>(x => new AuthProvider(
            x.GetRequiredService<SignInManager<User>>(),
            x.GetRequiredService<UserManager<User>>(),
            x.GetRequiredService<IHttpClientFactory>(),
            x.GetRequiredService<IMapper>(),
            settings.IdentityServerUri,
            settings.ClientId,
            settings.ClientSecret));
    }
}