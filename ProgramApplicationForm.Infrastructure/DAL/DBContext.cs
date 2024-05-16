using Microsoft.Azure.Cosmos;
using ProgramApplicationForm.Infrastructure.Common;

namespace ProgramApplicationForm.Infrastructure.DAL
{
    public class DBContext
    {
        private readonly CosmosClient _cosmosClient;
        private Database _database;

        public DBContext(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
        }

        public async Task InitializeAsync()
        {
            try
            {
                _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(DataConstants.AppDB);
                await _database.CreateContainerIfNotExistsAsync(DataConstants.pafContainerId, "/id");
                await _database.CreateContainerIfNotExistsAsync(DataConstants.questContainerId, "/id");
                await _database.CreateContainerIfNotExistsAsync(DataConstants.appsContainerId, "/id");
            }
            catch (CosmosException ex)
            {
                // Handle Cosmos DB specific exceptions
                Console.WriteLine($"Cosmos DB Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public Container GetContainer(string containerId)
        {
            if (_database == null)
            {
                throw new InvalidOperationException("Database has not been initialized. Call InitializeAsync first.");
            }

            return _database.GetContainer(containerId);
        }
    }
}