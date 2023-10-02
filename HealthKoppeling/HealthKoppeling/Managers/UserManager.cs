using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;

namespace HealthKoppeling.Managers
{
    public class UserManager: IManager<UserModel>
    {
        private readonly ICosmosDbService<UserModel> cosmosDbService;
        private List<UserModel> users;
        public UserManager(ICosmosDbService<UserModel> cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
            users = cosmosDbService.GetAllAsync().Result;
        }

        public async void Add(UserModel item)
        {
            users.Add(item);
            await cosmosDbService.AddAsync(item);
        }

        public bool CheckIfExists(UserModel item)
        {
            if (users.Count > 0)
            {
                foreach (UserModel user in users)
                {
                    if (user.Email == item.Email)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<UserModel> Get()
        {
            return this.users;
        }

        public void Remove(UserModel item)
        {
            foreach (UserModel user in users)
            {
                if (user.Email == item.Email)
                {
                    users.Remove(user);
                }
            }
        }

        public void Update(UserModel item)
        {
            throw new NotImplementedException();
        }
    }
}
