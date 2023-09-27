using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;

namespace HealthKoppeling.Managers
{
    public class UserManager: IManager<UserModel>
    {
        private List<UserModel> users;
        public UserManager()
        {
            users = new List<UserModel>();
        }

        public void Add(UserModel item)
        {
            UserModel newUser = new UserModel(item.name, item.email, item.accessToken);
            users.Add(newUser);
        }

        public bool CheckIfExists(UserModel item)
        {
            if (users.Count > 0)
            {
                foreach (UserModel user in users)
                {
                    if (user.email == item.email)
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
                if (user.email == item.email)
                {
                    users.Remove(user);
                }
            }
        }
    }
}
