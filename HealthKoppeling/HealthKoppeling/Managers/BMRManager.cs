using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;

namespace HealthKoppeling.Managers
{
    public class BMRManager: IManager<BMRModel>
    {
        private ICosmosDbService<BMRModel> cosmosDbService;
        private List<BMRModel> burnedCalories;

        public BMRManager(ICosmosDbService<BMRModel> cosmosDbService) 
        {
            this.cosmosDbService = cosmosDbService;
            this.burnedCalories = cosmosDbService.GetAllAsync().Result;
        }

        public async void Add(BMRModel item)
        {
            burnedCalories.Add(item);
            await cosmosDbService.AddAsync(item);
        }

        public bool CheckIfExists(BMRModel item)
        {
            if (burnedCalories.Count != 0)
            {
                foreach (BMRModel calories in burnedCalories)
                {
                    if (calories.StartTime == item.StartTime)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<BMRModel> Get()
        {
            return this.burnedCalories;
        }

        public async void Update(BMRModel item)
        {
            if (burnedCalories.Count != 0)
            {
                foreach (BMRModel calorie in burnedCalories)
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
