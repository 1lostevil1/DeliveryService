using AutoMapper;
using DeliveryServiceBL.Exceptions.PermissionsExceptions;
using DeliveryServiceBL.Exceptions.UsersExceptions;
using DeliveryServiceDataAccess.Entities;
using DeliveryServiceDL.Entity;

namespace DeliveryServiceDL.User.Manager;

public class UsersManager (
    IRepository<DeliveryServiceDataAccess.Entities.User> usersRepository,
    IRepository<PermissionEntity> permissionsRepository,
    IMapper mapper)
    : IUsersManager
{
    private readonly IRepository<DeliveryServiceDataAccess.Entities.User> _usersRepository;
    private readonly IMapper _mapper;

    public UserModel UpdateUsersPermissions(int id, UpdateUsersPermissionsModel updateModel)
    {
        var entity = usersRepository.GetById(id);
        if (entity is null)
            throw new UserNotFoundException("Такого пользователя не существует");

        var permissions = new List<PermissionEntity>();
        foreach (var permissionId in updateModel.Permissions)
        {
            var permissionEntity = permissionsRepository.GetById(permissionId);
            if (permissionEntity is not null)
                permissions.Add(permissionEntity);
        }

        if (permissions.Count == 0)
            throw new PermissionNotFoundException("Таких прав доступа не существует");

        entity.Permissions = permissions;
        entity = usersRepository.Save(entity);
        return mapper.Map<UserModel>(entity);
    }



    public UserModel CreateUser(CreateUserModel createModel)
    {
        var entity = _mapper.Map<DeliveryServiceDataAccess.Entities.User>(createModel);
        try
        {
            entity = _usersRepository.Save(entity);
            return _mapper.Map<UserModel>(entity);
        }
        catch (Exception e)
        {
            throw new UserAlreadyExistsException("Пользователь с такими данными уже существует");
        }
    }

    public void DeleteUser(int id)
    {
        var entity = _usersRepository.GetById(id);
        if (entity is null)
            throw new UserNotFoundException("Такого пользователя не существует");
        
        _usersRepository.Delete(entity);
    }

    public UserModel UpdateUser(int id, UpdateUserModel updateModel)
    {
        var entity = _usersRepository.GetById(id);
        if (entity is null)
            throw new UserNotFoundException("Такого пользователя не существует");
        
        entity = _mapper.Map<UpdateUserModel, DeliveryServiceDataAccess.Entities.User>(updateModel, opts => opts.AfterMap(
            (src, dest) =>
            {
                dest.Id = entity.Id;
                dest.ExternalId = entity.ExternalId;
                dest.CreationTime = entity.CreationTime;
                dest.ModificationTime = entity.ModificationTime;

                dest.Phone= src.PhoneNumber == null ? entity.Phone : dest.Phone;
                dest.EMail= src.Email == null ? entity.EMail : dest.EMail;
                dest.Name = src.Name == null ? entity.Name : dest.Name;
                dest.Surname = src.SurName == null ? entity.Surname : dest.Name;
            }));
        try
        {
            entity = _usersRepository.Save(entity);
            return _mapper.Map<UserModel>(entity);
        }
        catch (Exception e)
        {
            throw new UserAlreadyExistsException("Пользователь с такими данными уже существует");
        }
    }
}