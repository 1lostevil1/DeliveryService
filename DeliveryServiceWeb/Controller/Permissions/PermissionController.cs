using AutoMapper;
using DeliveryServiceBL.Permissions.Entity;
using DeliveryServiceBL.Permissions.Provider;
using DeliveryServiceWeb.Controller.Permissions.Entities.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryServiceWeb.Controller.Permissions.Entities;

[ApiController]
[Route("[controller]")]
public class PermissionController(
    IPermissionsProvider permissionsProvider,
    IMapper mapper,
    ILogger logger)
    : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllPermissions()
    {
        try
        {
            var permissions = permissionsProvider.GetPermissions();
            return Ok(new PermissionListResponse()
            {
                Permissions = permissions.ToList()
            });
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return BadRequest("Что-то пошло не так, повторите попытку позже");
        }
    }
    
    [HttpGet]
    [Route("filter")]
    public IActionResult GetFilteredPermissions([FromQuery] FilterPermission filter)
    {
        try
        {
            var filterModel = mapper.Map<FilterPermissionModel>(filter);
            var permissions = permissionsProvider.GetPermissions(filterModel);
            return Ok(new PermissionListResponse()
            {
                Permissions = permissions.ToList()
            });
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
            return BadRequest("Что-то пошло не так, повторите попытку позже");
        }
    }
}