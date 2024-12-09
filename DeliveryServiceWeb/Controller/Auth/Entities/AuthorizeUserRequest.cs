namespace DeliveryServiceWeb.Controller.Auth.Entities;

public class AuthorizeUserRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}