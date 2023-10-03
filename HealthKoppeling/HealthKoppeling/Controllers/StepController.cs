using Microsoft.AspNetCore.Http;
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

        [HttpPost]
        public JsonResult CreateStep(StepRequest stepRequest)
        {
            StepModel newStep = new StepModel(stepRequest.DailySteps, stepRequest.StartTimeNanos, stepRequest.EndTimeNanos, stepRequest.UserEmail);
            if (stepManager.CheckIfExists(newStep))
            {
                stepManager.Update(newStep);
                return new JsonResult("step exists");
            }
            else
            {
                stepManager.Add(newStep);
                return new JsonResult(Ok());
            }
        }
    }
}
