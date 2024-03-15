namespace DataRetention.Service.IntegrationTests.Infrastructure;

/// <summary>
/// This class can be used to interact with the integration test database. It can take care of recreating the database (i.e. dropping and creating it), migrating it to the latest version, 
/// resetting it to an empty state and creating sessions to insert and retrieve data as part of the integration tests.
/// </summary>
public class Database
{
    private readonly DatabaseOptions _databaseOptions;

    private Respawner _respawner;

    public Database(IOptions<DatabaseOptions> databaseOptions)
    {
        _databaseOptions = databaseOptions.Value;
    }

    public async Task ResetAsync()
    {
        if (_respawner != null)
        {
            await _respawner.ResetAsync(_databaseOptions.ConnectionString);
        }
    }

    public async Task RecreateAndMigrateAsync()
    {
        (var connectionStringWithoutDatabasePart, var databaseName) = SplitDatabaseNameFromConnectionString(_databaseOptions.ConnectionString);

        using var connection = new SqlConnection(connectionStringWithoutDatabasePart);
        await connection.OpenAsync();

        var databaseAlreadyExists = await DoesDatabaseAlreadyExistAsync(connection, databaseName);
        if (databaseAlreadyExists)
        {
            await DropDatabaseAsync(connection, databaseName);
        }

        await CreateDatabaseAsync(_databaseOptions.ConnectionString);

        _respawner = await Respawner.CreateAsync(_databaseOptions.ConnectionString, new RespawnerOptions
        {
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
        });
    }

    private static (string connectionStringWithoutDatabasePart, string databaseName) SplitDatabaseNameFromConnectionString(string connectionString)
    {
        var matchedDatabasePartInConnectionString = Regex.Match(connectionString, @"Database=([^\s;]+);", RegexOptions.IgnoreCase);
        var connectionStringWithoutDatabasePart = connectionString.Replace(matchedDatabasePartInConnectionString.Value, string.Empty);
        var databaseName = matchedDatabasePartInConnectionString.Groups[1].Value;
        return (connectionStringWithoutDatabasePart, databaseName);
    }

    private static async Task<bool> DoesDatabaseAlreadyExistAsync(SqlConnection connection, string databaseName)
    {
        using var queryCommand = new SqlCommand($"SELECT * FROM sys.databases WHERE name = '{databaseName}'", connection);
        using var reader = await queryCommand.ExecuteReaderAsync();
        return await reader.ReadAsync();
    }

    private static async Task DropDatabaseAsync(SqlConnection connection, string databaseName)
    {            
        using var dropDbCommand = new SqlCommand($"DROP DATABASE [{databaseName}]", connection);
        dropDbCommand.ExecuteNonQuery();
    }

    private static async Task CreateDatabaseAsync(string connection)
    {
        var contextOptions = new DbContextOptionsBuilder<NewDogTinderContext>()
            .UseSqlServer(connection)
            .Options;

        using var context = new NewDogTinderContext(contextOptions);
        context.Database.Migrate();
    }
}
