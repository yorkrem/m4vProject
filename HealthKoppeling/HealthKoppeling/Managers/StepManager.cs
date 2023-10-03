using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;

namespace HealthKoppeling.Managers
{
    public class StepManager: IManager<StepModel>
    {
        private readonly ICosmosDbService<StepModel> cosmosDbService;
        private List<StepModel> steps;

        public StepManager(ICosmosDbService<StepModel> cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
            this.steps = cosmosDbService.GetAllAsync().Result;
        }

        public async void Add(StepModel item)
        {
            steps.Add(item);
            await cosmosDbService.AddAsync(item);
        }

        public bool CheckIfExists(StepModel item)
        {
            if(steps.Count != 0) 
            { 
                foreach(StepModel step in steps)
                {
                    if(step.StartTime == item.StartTime)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public List<StepModel> Get()
        {
            return this.steps;
        }

        public void Remove(StepModel item)
        {
            if(steps.Count != 0)
            {
                foreach(StepModel step in steps)
                {
                    if(step.id == item.id)
                    {
                        steps.Remove(step);
                    }
                }
            }
        }

        public async void Update(StepModel item)
        {
            if(steps.Count != 0)
            {
                foreach(StepModel step in steps)
                {
                    if(step.StartTime == item.StartTime)
                    {
                        step.SetSteps(item.DailySteps);
                        await cosmosDbService.UpdateAsync(step.id, step);
                    }
                }
            }
        }
    }
}
