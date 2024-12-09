using DeliveryServiceBL.Permissions.Entity;

namespace DeliveryServiceBL.Permissions.Provider;

public interface IPermissionsProvider
{
    IEnumerable<PermissionModel> GetPermissions(FilterPermissionModel filter = null);
}