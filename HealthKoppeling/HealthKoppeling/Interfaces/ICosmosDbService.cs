using HealthKoppeling.Models;

namespace HealthKoppeling.Interfaces
{
    public interface ICosmosDbService
    {
        //Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task AddUserAsync(UserModel user);
    }
}