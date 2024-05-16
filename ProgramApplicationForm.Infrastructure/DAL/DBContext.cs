using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using ProgramApplicationForm.Infrastructure.Common;
namespace ProgramApplicationForm.Infrastructure.DAL;


public class DBContext
{
    //private readonly CosmosClient cosmosClient;
    //private Database _database;

    //public DBContext(CosmosClient cosmosClient)
    //{
    //    this.cosmosClient = cosmosClient;

    //}

    //public async Task InitializeAsync()
    //{
    //    string databaseId = DataConstants.AppDB;
    //    _database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);


    //    await _database.CreateContainerIfNotExistsAsync(DataConstants.pafContainerId, "/id");
    //    await _database.CreateContainerIfNotExistsAsync(DataConstants.questContainerId, "/id");
    //    await _database.CreateContainerIfNotExistsAsync(DataConstants.appsContainerId, "/id");

    //}



    private readonly CosmosClient _cosmosClient;
    private readonly Database _database;

    private static bool _initialized = false;
    private static readonly object _lock = new object();

    public DBContext(CosmosClient cosmosClient)
    {
        _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
        _database = _cosmosClient.GetDatabase(DataConstants.AppDB);
    }

    //public async Task EnsureInitializedAsync()
    //{
    //    if (!_initialized)
    //    {
    //        lock (_lock)
    //        {
    //            if (!_initialized)
    //            {
    //                 InitializeAsync();
    //                _initialized = true;
    //            }
    //        }
    //    }
    //}

    public async Task InitializeAsync()
    {
        await _database.CreateContainerIfNotExistsAsync(DataConstants.pafContainerId, "/id");
        await _database.CreateContainerIfNotExistsAsync(DataConstants.questContainerId, "/id");
        await _database.CreateContainerIfNotExistsAsync(DataConstants.appsContainerId, "/id");
    }

    public Container GetContainer(string containerId)
    {
        return _database.GetContainer(containerId);
    }
} 