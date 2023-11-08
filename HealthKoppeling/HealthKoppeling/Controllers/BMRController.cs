using HealthKoppeling.Interfaces;
using HealthKoppeling.Managers;
using HealthKoppeling.Models;
using HealthKoppeling.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthKoppeling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BMRController : ControllerBase
    {
        private IManager<BMRModel> bmrManager;

        public BMRController(IManager<BMRModel> manager)
        {
            this.bmrManager = manager;
        }

        [HttpPost]
        public JsonResult createBMR(BMRRequest burnedCaloriesRequest)
        {
            BMRModel bmr = new BMRModel(burnedCaloriesRequest.Calories, burnedCaloriesRequest.StartTime, burnedCaloriesRequest.EndTime);
            if (bmrManager.CheckIfExists(bmr))
            {
                bmrManager.Update(bmr);
                return new JsonResult("BMR updated");
            }
            else
            {
                bmrManager.Add(bmr);
                return new JsonResult(Ok());
            }

        }
    }
}
