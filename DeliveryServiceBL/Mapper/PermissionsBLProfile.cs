using AutoMapper;
using DeliveryServiceBL.Permissions.Entity;
using DeliveryServiceDataAccess.Entities;

namespace DeliveryServiceDL.Mapper;

public class PermissionsBLProfile : Profile
{
    public PermissionsBLProfile()
    {
        CreateMap<PermissionEntity, PermissionModel>()
            .ForMember(x => x.Id, y => y.MapFrom(src => src.Id));
    }
}