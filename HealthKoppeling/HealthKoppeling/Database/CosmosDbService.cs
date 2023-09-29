using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;
using Microsoft.Azure.Cosmos;

namespace HealthKoppeling.Database
{
    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;
        public CosmosDbService(CosmosClient cosmosClient, string databasename, string containername)
        {
            _container = cosmosClient.GetContainer(databasename, containername);


        }
        public async Task AddUserAsync(UserModel user)
        {
            await _container.CreateItemAsync(user, new PartitionKey(user.id));
        }


    }
}
