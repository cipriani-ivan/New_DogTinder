using System.Net.Http.Json;

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

    protected async Task<T> InsertInDatabaseAsync<T>(T entityToInsert)
    {
        using var connection = _database.OpenConnection();
        connection.Add(entityToInsert);
        await connection.SaveChangesAsync();
        return entityToInsert;
    }

    protected static async Task<T> ParseResponseContentAsync<T>(HttpResponseMessage response)
    {
        response.Content.Should().NotBeNull();

        var deserializedContent = await response.Content.ReadFromJsonAsync<T>();
        deserializedContent.Should().NotBeNull();

        return deserializedContent;
    }
}
