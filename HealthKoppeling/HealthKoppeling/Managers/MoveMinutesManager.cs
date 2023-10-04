using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;

namespace HealthKoppeling.Managers
{
    public class MoveMinutesManager: IManager<MoveMinutesModel>
    {
        private readonly ICosmosDbService<MoveMinutesModel> cosmosDbService;
        private List<MoveMinutesModel> moveMinutes;

        public MoveMinutesManager(ICosmosDbService<MoveMinutesModel> cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
            this.moveMinutes = cosmosDbService.GetAllAsync().Result;
        }

        public void Add(MoveMinutesModel item)
        {
            moveMinutes.Add(item);
            cosmosDbService.AddAsync(item);
        }

        public bool CheckIfExists(MoveMinutesModel item)
        {
            if (moveMinutes.Count != 0)
            {
                foreach (MoveMinutesModel mm in moveMinutes)
                {
                    if (mm.StartTime == item.StartTime)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<MoveMinutesModel> Get()
        {
            return this.moveMinutes;
        }

        public void Remove(MoveMinutesModel item)
        {
            if (moveMinutes.Count != 0)
            {
                foreach (MoveMinutesModel mm in moveMinutes)
                {
                    if (mm.id == item.id)
                    {
                        moveMinutes.Remove(mm);
                    }
                }
            }
        }

        public async void Update(MoveMinutesModel item)
        {
            if (moveMinutes.Count != 0)
            {
                foreach (MoveMinutesModel mm in moveMinutes)
                {
                    if (mm.StartTime == item.StartTime)
                    {
                        mm.SetMoveMinutes(item.moveMinutes);
                        await cosmosDbService.UpdateAsync(mm.id, mm);
                    }
                }
            }
        }
    }
}
