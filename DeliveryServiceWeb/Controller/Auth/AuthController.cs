using AutoMapper;
using DeliveryServiceBL.Auth;
using DeliveryServiceBL.Auth.Entities;
using DeliveryServiceWeb.Controller.User;
using DeliveryServiceWeb.Validator;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryServiceWeb.Controller.Auth.Entities;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthProvider authProvider, IMapper mapper, ILogger logger) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        try
        {
            var validationResult = await new RegisterUserRequestValidator().ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var registerModel = mapper.Map<RegisterUserModel>(request);
            
            var userModel = await authProvider.RegisterUser(registerModel);
            return Ok(new UsersListResponse
            {
                Users = [userModel]
            });
        }
        catch (ApplicationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return BadRequest("Что-то пошло не так, повторите позже)");
        }
    }
    
    [HttpGet]
    [Route("authorize")]
    public async Task<IActionResult> AuthorizeUser([FromQuery] AuthorizeUserRequest request)
    {
        try
        {
            var authRequest = mapper.Map<AuthorizeUserModel>(request);

            var tokens = await authProvider.AuthorizeUser(authRequest);

            return Ok(tokens);
        }
        catch (ApplicationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return BadRequest("Что-то пошло не так, повторите позже)");
        }
    }
}