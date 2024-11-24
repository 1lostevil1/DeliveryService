using AutoMapper;
using DeliveryServiceDL.Entity;
using DeliveryServiceDataAccess.Entities;


namespace DeliveryServiceDL.Mapper;

public class UsersBLProfile : Profile
{
    public UsersBLProfile()
    {
        CreateMap<DeliveryServiceDataAccess.Entities.User, UserModel>()
            .ForMember(x => x.Id, y => y.MapFrom(src => src.Id));
        
        CreateMap<CreateUserModel, DeliveryServiceDataAccess.Entities.User>()
            .ForMember(x => x.Id, y => y.Ignore())
            .ForMember(x => x.ExternalId, y => y.Ignore())
            .ForMember(x => x.CreationTime, y => y.Ignore())
            .ForMember(x => x.ModificationTime, y => y.Ignore())
            .ForMember(x => x.Name,
                y => y.MapFrom(src =>
                    src.Surname + " "+ src.Name ));

        CreateMap<UpdateUserModel, DeliveryServiceDataAccess.Entities.User>()
            .ForMember(x => x.Id, y => y.Ignore())
            .ForMember(x => x.ExternalId, y => y.Ignore())
            .ForMember(x => x.ModificationTime, y => y.Ignore())
            .ForMember(x => x.PasswordHash, y =>
                y.PreCondition(src => src.PasswordHash is not null))
            .ForMember(x => x.Phone, y =>
                y.PreCondition(src => src.PhoneNumber is not null))
            .ForMember(x => x.Name, y =>
                y.PreCondition(src => src.Name is not null))
            .ForMember(x => x.Surname, y =>
            y.PreCondition(src => src.SurName is not null));
        CreateMap<CreateUserModel, FilterUserModel>();
    }
}