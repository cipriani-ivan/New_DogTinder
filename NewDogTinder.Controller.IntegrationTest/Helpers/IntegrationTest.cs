namespace NewDogTinder.Controller.IntegrationTest.Helpers;

public abstract class IntegrationTest
{
    protected readonly WebApplicationFactory<Program> _factory;
    protected readonly HttpClient _client;
    protected readonly Database _database;

    protected IntegrationTest()
    {
        _factory = TestWebApplicationFactory.Instance;
        _database = _factory.Services.GetRequiredService<Database>();
        _client = _factory.CreateClient();
    }

    [TestInitialize]
    public async Task ResetDatabaseAndHttpClientSpyAsync()
    {
        await _database.ResetAsync();
    }
}
