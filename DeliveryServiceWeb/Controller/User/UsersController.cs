using AutoMapper;
using DeliveryServiceBL.User.Provider;
using DeliveryServiceDL.Entity;
using DeliveryServiceDL.User.Manager;
using DeliveryServiceWeb.Controller.User;
using DeliveryServiceWeb.Validator;
using Microsoft.AspNetCore.Mvc;


namespace DeliveryServiceWeb.Controller;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersManager _usersManager;
    private readonly IUsersProvider _usersProvider;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public UsersController(IUsersManager usersManager, IUsersProvider usersProvider,
        IMapper mapper, ILogger logger)
    {
        _usersManager = usersManager;
        _usersProvider = usersProvider;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [Route("register")]
    public IActionResult RegisterUser([FromBody] RegisterUserRequest request)
    {
        var validationResult = new RegisterUserRequestValidator().Validate(request);
        if (validationResult.IsValid)
        {
            var createUserModel = _mapper.Map<CreateUserModel>(request);
            try
            {
                var userModel = _usersManager.CreateUser(createUserModel);
                return Ok(new UsersListResponse
                {
                    Users = [userModel]
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        _logger.LogError(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }

    [HttpPost]
    [Route("update")]
    public IActionResult UpdateUserInfo([FromQuery] UpdateUserRequest request)
    {
        var validationResult = new UpdateUserRequestValidator().Validate(request);
        if (validationResult.IsValid)
        {
            var updateUserModel = _mapper.Map<UpdateUserModel>(request);
            try
            {
                var userModel = _usersManager.UpdateUser(request.Id, updateUserModel);
                return Ok(new UsersListResponse
                {
                    Users = [userModel]
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return BadRequest(e.Message);
            }
        }
        
        _logger.LogError(validationResult.ToString());
        return BadRequest(validationResult.ToString());
    }

    [HttpPost]
    [Route("unregister")]
    public IActionResult UnregisterUser([FromQuery] int userIdToUnregister)
    {
        try
        {
            _usersManager.DeleteUser(userIdToUnregister);
            return Ok("Пользователь был удален");
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = _usersProvider.GetUsers();
        return Ok(new UsersListResponse
        {
            Users = users.ToList()
        });
    }

    [HttpGet]
    [Route("filter")]
    public IActionResult GetFilteredUsers([FromQuery] UserFilter filter)
    {
        var userFilterModel = _mapper.Map<FilterUserModel>(filter);
        var users = _usersProvider.GetUsers(userFilterModel);
        return Ok(new UsersListResponse
        {
            Users = users.ToList()
        });
    }
}