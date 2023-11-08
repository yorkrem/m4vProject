using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;
using Microsoft.Azure.Cosmos;

namespace HealthKoppeling.Database
{
    public class MoveMinutesDbService: ICosmosDbService<MoveMinutesModel>
    {
        private Container _container;
        public MoveMinutesDbService(CosmosClient cosmosClient, string databasename)
        {
            _container = cosmosClient.GetContainer(databasename, "MoveMinutesContainer");
        }

        public async Task AddAsync(MoveMinutesModel item)
        {
            await _container.CreateItemAsync(item, new PartitionKey(item.id));
        }

        public async Task<List<MoveMinutesModel>> GetAllAsync()
        {
            var query = _container.GetItemQueryIterator<MoveMinutesModel>(new QueryDefinition("SELECT * FROM MoveMinutesContainer"));
            var results = new List<MoveMinutesModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, MoveMinutesModel item)
        {
            await _container.UpsertItemAsync(item, new PartitionKey(id));
        }
    }
}
