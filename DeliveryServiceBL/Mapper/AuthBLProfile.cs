using AutoMapper;
using DeliveryServiceBL.Auth.Entities;

namespace DeliveryServiceDL.Mapper;

public class AuthBLProfile : Profile
{
    public AuthBLProfile()
    {
        CreateMap<RegisterUserModel, DeliveryServiceDataAccess.Entities.User>()
            .ForMember(x => x.Id, y => y.Ignore())
            .ForMember(x => x.ExternalId, y => y.Ignore())
            .ForMember(x => x.CreationTime, y => y.Ignore())
            .ForMember(x => x.ModificationTime, y => y.Ignore())
            .ForMember(x => x.Name,
                y => y.MapFrom(src =>
                    src.Surname + " " + src.Name + (src.Patronymic == null ? string.Empty : " " + src.Patronymic)))
            .ForMember(x => x.Permissions, y => y.Ignore());
    }
}