﻿using HealthKoppeling.Interfaces;
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

        public bool CheckIfExists(string startdate)
        {
            if(steps.Count != 0) 
            { 
                foreach(StepModel step in steps)
                {
                    if(step.StartDate == startdate)
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

        public StepModel GetByDate(string date)
        {
            if (steps.Count != 0)
            {
                foreach (StepModel step in steps)
                {
                    if (step.StartDate == date)
                    {
                        return step;
                    }
                }
            }
            return null;
        }

        public async void Update(StepModel item)
        {
            if(steps.Count != 0)
            {
                foreach(StepModel step in steps)
                {
                    if(step.StartDate == item.StartDate)
                    {
                        step.SetSteps(item.DailySteps);
                        await cosmosDbService.UpdateAsync(step.id, step);
                    }
                }
            }
        }
    }
}
