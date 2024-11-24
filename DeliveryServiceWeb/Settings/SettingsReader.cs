namespace DeliveryServiceWeb.Settings;

public class SettingsReader
{
    public static Settings Read(IConfiguration configuration)
    {
        return new Settings()
        {
            ServiceUri = configuration.GetValue<Uri>("Uri"),
            DbContextConnectionString = configuration.GetValue<string>("HotelChainDbContext")
        };
    }
}