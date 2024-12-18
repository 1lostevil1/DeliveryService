﻿using DeliveryServiceDL.Entity;

namespace DeliveryServiceDL.User.Manager;

public interface IUsersManager
{
    UserModel CreateUser(CreateUserModel createModel);
    void DeleteUser(int id);
    UserModel UpdateUser(int id, UpdateUserModel updateModel);
    
    UserModel UpdateUsersPermissions(int id, UpdateUsersPermissionsModel updateModel);
}