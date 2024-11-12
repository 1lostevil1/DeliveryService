using AutoMapper;
using DeliveryServiceDL.Entity;
using DeliveryServiceDataAccess.Entities;


namespace DeliveryServiceDL.Mapper;

public class UsersBLProfile : Profile
{
    public UsersBLProfile()
    {
        CreateMap<DeliveryServiceDataAccess.Entities.User, UserModel>()
            .ForMember(x => x.Id, y => y.MapFrom(src => src.Id))
            .ForMember(x => x.ExternalId, y => y.Map
        From(src => src.ExternalId));

        CreateMap<CreateUserModel, DeliveryServiceDataAccess.Entities.User>()
            .ForMember(x => x.Id, y => y.Ignore())
            .ForMember(x => x.ExternalId, y => y.Ignore())
            .ForMember(x => x.CreationTime, y => y.Ignore())
            .ForMember(x => x.ModificationTime, y => y.Ignore());

        CreateMap<UpdateUserModel, DeliveryServiceDataAccess.Entities.User>()
            .ForMember(x => x.Id, y => y.MapFrom(src => src.Id))
            .ForMember(x => x.ExternalId, y => y.MapFrom(src => src.ExternalId))
            .ForMember(x => x.ModificationTime, y => y.Ignore());
    }
}