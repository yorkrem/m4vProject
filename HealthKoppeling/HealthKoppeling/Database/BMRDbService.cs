using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;
using Microsoft.Azure.Cosmos;

namespace HealthKoppeling.Database
{
    public class BMRDbService: ICosmosDbService<BMRModel>
    {
        private Container _container;

        public BMRDbService(CosmosClient cosmosClient, string databasename)
        {
            _container = cosmosClient.GetContainer(databasename, "BMRContainer");
        }

        public async Task AddAsync(BMRModel item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.id));
        }

        public async Task<List<BMRModel>> GetAllAsync()
        {
            var query = _container.GetItemQueryIterator<BMRModel>(new QueryDefinition("SELECT * FROM BMRContainer"));
            var results = new List<BMRModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, BMRModel item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }
    }
}
