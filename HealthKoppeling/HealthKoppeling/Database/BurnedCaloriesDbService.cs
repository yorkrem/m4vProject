using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;
using Microsoft.Azure.Cosmos;

namespace HealthKoppeling.Database
{
    public class BurnedCaloriesDbService : ICosmosDbService<BurnedCaloriesModel>
    {
        private Container _container;

        public BurnedCaloriesDbService(CosmosClient cosmosClient, string databasename)
        {
            _container = cosmosClient.GetContainer(databasename, "BurnedCaloriesContainer");
        }
        public async Task AddAsync(BurnedCaloriesModel item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.id));
        }

        public async Task<List<BurnedCaloriesModel>> GetAllAsync()
        {
            var query = _container.GetItemQueryIterator<BurnedCaloriesModel>(new QueryDefinition("SELECT * FROM BurnedCaloriesContainer"));
            var results = new List<BurnedCaloriesModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, BurnedCaloriesModel item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }
    }
}
