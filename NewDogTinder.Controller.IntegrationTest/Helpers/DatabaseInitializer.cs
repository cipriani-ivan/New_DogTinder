namespace DataRetention.Service.IntegrationTests.Infrastructure;

[TestClass]
public static class DatabaseInitializer
{
    [AssemblyInitialize]
    public static async Task InitializeDatabaseAsync(TestContext _)
    {
        var factory = TestWebApplicationFactory.Instance;
        var database = factory.Services.GetRequiredService<Database>();
        await database.RecreateAndMigrateAsync();
    }
}
