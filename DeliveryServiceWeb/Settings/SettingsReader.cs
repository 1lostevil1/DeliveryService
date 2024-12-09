namespace DeliveryServiceWeb.Settings;

public static class SettingsReader
{
    public static Settings Read(IConfiguration configuration)
    {
        return new Settings
        {
            DbContextConnectionString = configuration.GetValue<string>("HotelChainDbContext"),
            IdentityServerUri = configuration.GetValue<string>("IdentityServer:Uri"),
            ClientId = configuration.GetValue<string>("IdentityServer:ClientId"),
            ClientSecret = configuration.GetValue<string>("IdentityServer:ClientSecret"),
            ApiName = configuration.GetValue<string>("IdentityServer:ApiName"),
            MasterAdminData = new (
                configuration.GetValue<string>("IdentityServer:MasterAdminData:UserName"),
                configuration.GetValue<string>("IdentityServer:MasterAdminData:Password"))
        };
    }
}