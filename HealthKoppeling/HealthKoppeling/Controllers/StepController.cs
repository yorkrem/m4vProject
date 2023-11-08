﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HealthKoppeling.Requests;
using HealthKoppeling.Models;
using HealthKoppeling.Interfaces;

namespace HealthKoppeling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepController : ControllerBase
    {
        private IManager<StepModel> stepManager;

        public StepController(IManager<StepModel> stepManager)
        {
            this.stepManager = stepManager;
        }

        [HttpGet]
        public JsonResult Get(string startdate)
        {
            try
            {
                StepModel step = stepManager.GetByDate(startdate);
                if (step != null)
                {
                    return new JsonResult(step);
                }
                else
                {
                    throw new Exception("step data does not exist for this date");
                }
            }
            catch (Exception) 
            {
                return new JsonResult("step data does not exist for this date");
            }
        }

        [HttpPost]
        public JsonResult CreateStep(StepRequest stepRequest)
        {
            StepModel newStep = new StepModel(stepRequest.DailySteps, stepRequest.StartDate, stepRequest.EndDate);
            if (stepManager.CheckIfExists(newStep))
            {
                stepManager.Update(newStep);
                return new JsonResult("step updated");
            }
            else
            {
                stepManager.Add(newStep);
                return new JsonResult(Ok());
            }
        }
    }
}
