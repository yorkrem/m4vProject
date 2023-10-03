using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;

namespace HealthKoppeling.Managers
{
    public class BurnedCaloriesManager: IManager<BurnedCaloriesModel>
    {
        private ICosmosDbService<BurnedCaloriesModel> cosmosDbService;
        private List<BurnedCaloriesModel> burnedCalories;

        public BurnedCaloriesManager(ICosmosDbService<BurnedCaloriesModel> cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
            this.burnedCalories = cosmosDbService.GetAllAsync().Result;
        }

        public void Add(BurnedCaloriesModel item)
        {
            burnedCalories.Add(item);
            cosmosDbService.AddAsync(item);
        }

        public bool CheckIfExists(BurnedCaloriesModel item)
        {
            if (burnedCalories.Count != 0)
            {
                foreach (BurnedCaloriesModel calories in burnedCalories)
                {
                    if (calories.StartTime == item.StartTime)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<BurnedCaloriesModel> Get()
        {
            return this.burnedCalories;
        }

        public void Remove(BurnedCaloriesModel item)
        {
            if (burnedCalories.Count != 0)
            {
                foreach (BurnedCaloriesModel calorie in burnedCalories)
                {
                    if (calorie.id == item.id)
                    {
                        burnedCalories.Remove(calorie);
                    }
                }
            }
        }

        public async void Update(BurnedCaloriesModel item)
        {
            if (burnedCalories.Count != 0)
            {
                foreach (BurnedCaloriesModel calorie in burnedCalories)
                {
                    if (calorie.StartTime == item.StartTime)
                    {
                        calorie.SetCalories(item.Calories);
                        await cosmosDbService.UpdateAsync(calorie.id, calorie);
                    }
                }
            }
        }
    }
}
