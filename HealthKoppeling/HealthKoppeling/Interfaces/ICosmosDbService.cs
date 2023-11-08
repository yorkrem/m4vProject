using HealthKoppeling.Models;

namespace HealthKoppeling.Interfaces
{
    public interface ICosmosDbService<T>
    {
        //Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task AddAsync(T item);
        Task UpdateAsync(string id, T item);
        Task<List<T>> GetAllAsync();
    }
}