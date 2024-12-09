using AutoMapper;
using DeliveryServiceBL.Auth.Entities;
using DeliveryServiceBL.Exceptions.AuthExceptions;
using DeliveryServiceDL.Entity;
using DeliveryServiceDL.User.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace DeliveryServiceBL.Auth;

public class AuthProvider(
    SignInManager<DeliveryServiceDataAccess.Entities.User> signInManager,
    UserManager<DeliveryServiceDataAccess.Entities.User> userManager,
    IHttpClientFactory httpClientFactory,
    IMapper mapper,
    string identityServerUri,
    string clientId,
    string clientSecret)
    : IAuthProvider
{
    public async Task<UserModel> RegisterUser(RegisterUserModel model)
    {
        var user = await userManager.FindByNameAsync(model.UserName);
        if (user is not null)
        {
            throw new UserAlreadyExistsException("Пользователь с таким именем уже существует");
        }

        user = mapper.Map<DeliveryServiceDataAccess.Entities.User>(model);
        user.ExternalId = Guid.NewGuid();
        user.CreationTime = DateTime.UtcNow;
        user.ModificationTime = DateTime.UtcNow;

        var createResult = await userManager.CreateAsync(user, model.Password);
        if (!createResult.Succeeded)
        {
            throw new WrongCreationUserDataException(createResult.Errors.Select(x => x.Description)
                                                                        .Aggregate((x, y) => x + " " + y));
        }

        user = await userManager.FindByNameAsync(model.UserName);
        return mapper.Map<UserModel>(user);
    }

    public async Task<TokensResponse> AuthorizeUser(AuthorizeUserModel model)
    {
        var userByName = await userManager.FindByNameAsync(model.UserName);
        if (userByName is null)
        {
            throw new UserNotFoundException("Пользователя с такими данными не существует");
        }

        var checkPasswordResult = await signInManager.CheckPasswordSignInAsync(userByName, model.Password, false);
        if (!checkPasswordResult.Succeeded)
        {
            throw new WrongPasswordException("Неверный пароль");
        }

        var client = httpClientFactory.CreateClient();
        var endpoints = await client.GetDiscoveryDocumentAsync(identityServerUri);
        if (endpoints.IsError)
        {
            throw new Exception(endpoints.Error);
        }

        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = endpoints.TokenEndpoint,
            ClientId = clientId,
            ClientSecret = clientSecret,
            UserName = model.UserName,
            Password = model.Password,
            Scope = "api offline_access"
        });
        if (tokenResponse.IsError)
        {
            throw new Exception(tokenResponse.Error);
        }

        return new TokensResponse
        {
            AccessToken = tokenResponse.AccessToken,
            RefreshToken = tokenResponse.RefreshToken
        };
    }
}