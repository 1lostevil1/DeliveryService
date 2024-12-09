using DeliveryServiceBL.Auth.Entities;
using DeliveryServiceDL.Entity;

namespace DeliveryServiceBL.Auth;

public interface IAuthProvider
{
    Task<UserModel> RegisterUser(RegisterUserModel model);
    Task<TokensResponse> AuthorizeUser(AuthorizeUserModel model);
}