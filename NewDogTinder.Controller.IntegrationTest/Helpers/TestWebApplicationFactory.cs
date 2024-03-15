namespace NewDogTinder.Controller.IntegrationTest.Helpers;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private static TestWebApplicationFactory _instance;

    public static TestWebApplicationFactory Instance
    {
        get
        {
            _instance ??= new TestWebApplicationFactory();
            return _instance;
        }
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.Development.json"));
        });

        builder.ConfigureTestServices(services =>
        {
            services.AddSingleton<Database>();

            services.AddAuthentication(defaultScheme: "TestScheme").AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });
        });
    }
}