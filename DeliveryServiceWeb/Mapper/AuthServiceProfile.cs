using AutoMapper;
using DeliveryServiceBL.Auth.Entities;
using DeliveryServiceWeb.Controller.Auth.Entities;

namespace DeliveryServiceWeb.Mapper;

public class AuthServiceProfile : Profile
{
    public AuthServiceProfile()
    {
        CreateMap<RegisterUserRequest, RegisterUserModel>();
        CreateMap<AuthorizeUserRequest, AuthorizeUserModel>();
    }
}