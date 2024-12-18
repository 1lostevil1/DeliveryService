namespace DefaultNamespace;

public class TestConfigurator
{
    private static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();
    }
    
    public static HotelChainSettings GetSettings()
    {
        return HotelChainSettingsReader.Read(GetConfiguration());
    }
}