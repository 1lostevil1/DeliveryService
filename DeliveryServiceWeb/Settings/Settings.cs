namespace DeliveryServiceWeb.Settings;

public class Settings
{
    public string DbContextConnectionString { get; set; }
    public string IdentityServerUri { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string ApiName { get; set; }
    public (string UserName, string Password) MasterAdminData { get; set; }
}