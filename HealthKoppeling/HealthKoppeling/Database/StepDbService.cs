using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;
using Microsoft.Azure.Cosmos;

namespace HealthKoppeling.Database
{
    public class StepDbService: ICosmosDbService<StepModel>
    {
        private Container _container;
        public StepDbService(CosmosClient cosmosClient, string databasename) 
        {
            _container = cosmosClient.GetContainer(databasename, "StepsContainer");
        }

        public async Task AddAsync(StepModel item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.id));
        }

        public async Task<List<StepModel>> GetAllAsync()
        {
            var query = _container.GetItemQueryIterator<StepModel>(new QueryDefinition("SELECT * FROM StepsContainer"));
            var results = new List<StepModel>();
            while(query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, StepModel item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }
    }
}
