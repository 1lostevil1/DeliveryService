using AutoMapper;
using DeliveryServiceBL.Permissions.Entity;
using DeliveryServiceDataAccess.Entities;

namespace DeliveryServiceBL.Permissions.Provider;

public class PermissionsProvider(IRepository<PermissionEntity> permissionsRepository, IMapper mapper)
    : IPermissionsProvider
{
    public IEnumerable<PermissionModel> GetPermissions(FilterPermissionModel? filter = null)
    {
        DateTime? creationTime = filter?.CreationTime;
        DateTime? modificationTime = filter?.ModificationTime;
        List<string>? typeParts = filter?.Types;

        var permissions = permissionsRepository.GetAll(p =>
            (creationTime == null || p.CreationTime == creationTime) &&
            (modificationTime == null || p.ModificationTime == modificationTime) &&
            (typeParts == null || typeParts.Any(x => p.Type.Contains(x))));
        return mapper.Map<IEnumerable<PermissionModel>>(permissions);
    }
}