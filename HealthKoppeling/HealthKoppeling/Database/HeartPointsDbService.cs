using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;
using Microsoft.Azure.Cosmos;

namespace HealthKoppeling.Database
{
    public class HeartPointsDbService : ICosmosDbService<HeartPointModel>
    {
        private Container _container;

        public HeartPointsDbService(CosmosClient cosmosClient, string databasename)
        {
            _container = cosmosClient.GetContainer(databasename, "HeartPointsContainer");
        }
        public async Task AddAsync(HeartPointModel item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.id));
        }

        public async Task<List<HeartPointModel>> GetAllAsync()
        {
            var query = _container.GetItemQueryIterator<HeartPointModel>(new QueryDefinition("SELECT * FROM HeartPointsContainer"));
            var results = new List<HeartPointModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, HeartPointModel item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }
    }
}
