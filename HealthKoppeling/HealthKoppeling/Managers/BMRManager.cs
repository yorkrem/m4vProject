using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;

namespace HealthKoppeling.Managers
{
    public class BMRManager: IManager<BMRModel>
    {
        private readonly ICosmosDbService<BMRModel> cosmosDbService;

        private List<BMRModel> bmrs;

        public BMRManager(ICosmosDbService<BMRModel> cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
            this.bmrs = cosmosDbService.GetAllAsync().Result;
        }

        public async void Add(BMRModel item)
        {
            bmrs.Add(item);
            await cosmosDbService.AddAsync(item);
        }

        public bool CheckIfExists(BMRModel item)
        {
            foreach(BMRModel bmr in bmrs)
            {
                if(bmr.EndTime == item.EndTime)
                {
                    return true;
                }
            }
            return false;
        }

        public List<BMRModel> Get()
        {
            return this.bmrs;
        }

        public async void Update(BMRModel item)
        {
            foreach (BMRModel bmr in bmrs)
            {
                if (bmr.EndTime == item.EndTime)
                {
                    bmr.SetBMR(item.Bmr);
                    await cosmosDbService.UpdateAsync(bmr.id, bmr);
                }
            }
        }
    }
}
