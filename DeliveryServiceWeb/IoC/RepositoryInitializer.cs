using DeliveryServiceBL.Auth;
using DeliveryServiceBL.Auth.Entities;
using DeliveryServiceBL.User.Provider;
using DeliveryServiceDataAccess;
using DeliveryServiceDataAccess.Entities;
using DeliveryServiceDL.Entity;
using DeliveryServiceDL.User.Manager;
using Microsoft.EntityFrameworkCore;

namespace DeliveryServiceWeb.Service.IoC;

public static class RepositoryInitializer
{
    private static async Task<List<PermissionEntity>> InitializePermissions(
        IDbContextFactory<DeliveryServiceDbContext> dbContextFactory)
    {
        var permissions = new List<PermissionEntity>();

        await using var context = await dbContextFactory.CreateDbContextAsync();

        var permissionEntity = await context.Permissions.FirstOrDefaultAsync(x => x.Type == "PrivateRead");
        if (permissionEntity == null)
        {
            var permission = await context.Permissions.AddAsync(new PermissionEntity
            {
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Type = "PrivateRead"
            });
            permissions.Add(permission.Entity);
        }
        else
        {
            permissions.Add(permissionEntity);
        }

        permissionEntity = await context.Permissions.FirstOrDefaultAsync(x => x.Type == "PrivateWrite");
        if (permissionEntity == null)
        {
            var permission = await context.Permissions.AddAsync(new PermissionEntity
            {
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Type = "PrivateWrite"
            });
            permissions.Add(permission.Entity);
        }
        else
        {
            permissions.Add(permissionEntity);
        }

        permissionEntity = await context.Permissions.FirstOrDefaultAsync(x => x.Type == "PublicRead");
        if (permissionEntity == null)
        {
            var permission = await context.Permissions.AddAsync(new PermissionEntity
            {
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Type = "PublicRead"
            });
            permissions.Add(permission.Entity);
        }
        else
        {
            permissions.Add(permissionEntity);
        }

        permissionEntity = await context.Permissions.FirstOrDefaultAsync(x => x.Type == "PublicWrite");
        if (permissionEntity == null)
        {
            var permission = await context.Permissions.AddAsync(new PermissionEntity
            {
                ExternalId = Guid.NewGuid(),
                CreationTime = DateTime.UtcNow,
                ModificationTime = DateTime.UtcNow,
                Type = "PublicWrite"
            });
            permissions.Add(permission.Entity);
        }
        else
        {
            permissions.Add(permissionEntity);
        }

        await context.SaveChangesAsync();
        return permissions;
    }

    private static async Task<UserModel> CreateGlobalAdmin(IAuthProvider authProvider, string userName, string password)
    {
        return await authProvider.RegisterUser(new RegisterUserModel
        {
            UserName = userName,
            Password = password
        });
    }

    private static void GrantPermissions(IUsersManager usersManager, int id, List<PermissionEntity> permissions)
    {
        usersManager.UpdateUsersPermissions(id, new UpdateUsersPermissionsModel
        {
            Permissions = permissions.Select(x => x.Id).ToList()
        });
    }

    public static async Task ConfigureApplication(IApplicationBuilder app, Settings.Settings settings)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        var dbContextFactory =
            (IDbContextFactory<DeliveryServiceDbContext>)scope.ServiceProvider.GetRequiredService(
                typeof(IDbContextFactory<DeliveryServiceDbContext>));
        var permissions = await InitializePermissions(dbContextFactory);

        var usersProvider = (IUsersProvider)scope.ServiceProvider.GetRequiredService(typeof(IUsersProvider));
        if (!usersProvider.GetUsers(new FilterUserModel { Login = settings.MasterAdminData.UserName }).Any())
        {
            var authProvider = (IAuthProvider)scope.ServiceProvider.GetRequiredService(typeof(IAuthProvider));
            var adminModel = await CreateGlobalAdmin(authProvider, settings.MasterAdminData.UserName,
                settings.MasterAdminData.Password);

            var usersManager = (IUsersManager)scope.ServiceProvider.GetRequiredService(typeof(IUsersManager));
            GrantPermissions(usersManager, adminModel.Id, permissions);
        }
    }
}