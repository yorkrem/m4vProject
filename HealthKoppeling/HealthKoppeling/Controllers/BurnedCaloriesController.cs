using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;
using HealthKoppeling.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthKoppeling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BurnedCaloriesController : ControllerBase
    {
        private IManager<BurnedCaloriesModel> burnedCaloriesManager;

        public BurnedCaloriesController(IManager<BurnedCaloriesModel> burnedCaloriesManager)
        {
            this.burnedCaloriesManager = burnedCaloriesManager;
        }

        [HttpGet]
        public JsonResult GetBurnedCalories(string startdate)
        {
            try
            {
                BurnedCaloriesModel burnedCalories = burnedCaloriesManager.GetByDate(startdate);
                if (burnedCalories != null)
                {
                    return new JsonResult(burnedCalories);
                }
                else
                {
                    throw new Exception("burned calorie data does not exist for this date");
                }
            }
            catch (Exception)
            {
                return new JsonResult("burned calories data does not exist for this ");
            }
           
        }

        [HttpPost]
        public JsonResult CreateBurnedCalories(BurnedCaloriesRequest burnedCaloriesRequest)
        {
            BurnedCaloriesModel burnedCalories = new BurnedCaloriesModel(burnedCaloriesRequest.Calories, burnedCaloriesRequest.StartTime, burnedCaloriesRequest.EndTime);
            if (burnedCaloriesManager.CheckIfExists(burnedCalories))
            {
                burnedCaloriesManager.Update(burnedCalories);
                return new JsonResult("burnedCalorie updated");
            }
            else
            {
                burnedCaloriesManager.Add(burnedCalories);
                return new JsonResult(Ok());
            }
            
        }
    }
}
