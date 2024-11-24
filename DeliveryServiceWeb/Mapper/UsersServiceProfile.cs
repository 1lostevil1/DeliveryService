using AutoMapper;
using DeliveryServiceDL.Entity;
using DeliveryServiceWeb.Controller.User;

namespace DeliveryServiceWeb.Mapper;

public class UsersServiceProfile : Profile
{
    public UsersServiceProfile()
    {
        CreateMap<RegisterUserRequest, CreateUserModel>();
        CreateMap<UpdateUserRequest, UpdateUserModel>();
        CreateMap<UserFilter, FilterUserModel>();
    }
}