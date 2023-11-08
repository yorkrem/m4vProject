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

        [HttpGet]
        public StepModel Get(string startdate)
        {
            StepModel step = stepManager.GetByDate(startdate);
            if (step != null)
            {
                return step;
            }
            else
            {
                throw new Exception("step data for this date does not exist");
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
