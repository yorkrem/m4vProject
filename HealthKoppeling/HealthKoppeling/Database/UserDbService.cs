using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;
using Microsoft.Azure.Cosmos;

namespace HealthKoppeling.Database
{
    public class UserDbService : ICosmosDbService<UserModel>
    {
        private Container _container;
        public UserDbService(CosmosClient cosmosClient, string databasename)
        {
            _container = cosmosClient.GetContainer(databasename, "UserContainer");
        }
        public async Task AddAsync(UserModel user)
        {
            await _container.CreateItemAsync(user, new PartitionKey(user.id));
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            var query = _container.GetItemQueryIterator<UserModel>(new QueryDefinition("SELECT * FROM UserContainer"));
            var results = new List<UserModel>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task UpdateAsync(string id, UserModel item)
        {
            await _container.UpsertItemAsync(item,new PartitionKey(id));
        }
    }
}
