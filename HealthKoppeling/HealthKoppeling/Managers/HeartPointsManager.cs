using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;

namespace HealthKoppeling.Managers
{
    public class HeartPointsManager: IManager<HeartPointModel>
    {
        private readonly ICosmosDbService<HeartPointModel> cosmosDbService;
        private List<HeartPointModel> heartPoints;

        public HeartPointsManager(ICosmosDbService<HeartPointModel> cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
            this.heartPoints = cosmosDbService.GetAllAsync().Result;
        }

        public async void Add(HeartPointModel item)
        {
            heartPoints.Add(item);
            await cosmosDbService.AddAsync(item);
        }

        public bool CheckIfExists(HeartPointModel item)
        {
            foreach(HeartPointModel heartPoint in heartPoints)
            {
                if(heartPoint.StartTime == item.StartTime)
                {
                    return true;
                }
            }
            return false;
        }

        public List<HeartPointModel> Get()
        {
            return this.heartPoints;
        }

        public async void Update(HeartPointModel item)
        {
            foreach(HeartPointModel heartPoint in heartPoints)
            {
                if(heartPoint.StartTime == item.StartTime)
                {
                    heartPoint.SetHeartPoint(item.HeartPoints);
                    await cosmosDbService.UpdateAsync(heartPoint.id, heartPoint);
                }
            }
        }
    }
}
