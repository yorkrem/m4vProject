using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;

namespace HealthKoppeling.Managers
{
    public class UserManager: IManager<UserModel>
    {
        private readonly ICosmosDbService _cosmosDbService;
        private IEnumerable<UserModel> _user;
        private List<UserModel> users;
        public UserManager(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
            users = new List<UserModel>();
        }

        public async void Add(UserModel item)
        {
            UserModel newUser = new UserModel(item.Name, item.Email, item.AccessToken);
            users.Add(newUser);
            await _cosmosDbService.AddUserAsync(newUser);
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
    }
}
