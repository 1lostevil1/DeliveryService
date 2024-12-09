using AutoMapper;
using DeliveryServiceBL.Permissions.Entity;
using DeliveryServiceWeb.Controller.Permissions.Entities.Entities;

namespace DeliveryServiceWeb.Mapper;

public class PermissionsServiceProfile : Profile
{
    public PermissionsServiceProfile()
    {
        CreateMap<FilterPermission, FilterPermissionModel>();
    }
}