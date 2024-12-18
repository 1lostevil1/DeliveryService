namespace DefaultNamespace;

public class TestWebApplicationFactory(Action<IServiceCollection>? overrideDependencies = null)
    : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services => overrideDependencies?.Invoke(services));
    }
}