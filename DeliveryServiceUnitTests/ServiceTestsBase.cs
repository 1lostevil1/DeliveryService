namespace DefaultNamespace;

public class ServiceTestsBase
{
    private readonly WebApplicationFactory<Program> _testServer;
    protected HttpClient TestHttpClient => _testServer.CreateClient();

    public HotelChainServiceTestsBase()
    {
        var settings = TestConfigurator.GetSettings();

        _testServer = new TestWebApplicationFactory(services =>
        {
            services.Replace(ServiceDescriptor.Scoped(_ =>
            {
                var httpClientFactoryMock = new Mock<IHttpClientFactory>();
                httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>()))
                    .Returns(TestHttpClient);
                return httpClientFactoryMock.Object;
            }));
            services.PostConfigureAll<JwtBearerOptions>(options =>
            {
                options.ConfigurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
                    $"{settings.IdentityServerUri}/.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever(),
                    new HttpDocumentRetriever(TestHttpClient)
                    {
                        RequireHttps = false,
                        SendAdditionalHeaderData = true
                    });
            });
        });
    }

    public T GetService<T>() where T : notnull
    {
        return _testServer.Services.GetRequiredService<T>();
    }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
    }

    [OneTimeTearDownAttribute]
    public void OneTimeTearDown()
    {
        _testServer.Dispose();
    }