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

        [HttpGet]
        public JsonResult GetBMR(string startdate)
        {
            try
            {
                BMRModel bmr = bmrManager.GetByDate(startdate);
                if (bmr != null)
                {
                    return new JsonResult(bmr);
                }
                else
                {
                    throw new Exception("bmr data does not exist for this date");
                }
            }
            catch(Exception) 
            {
                return new JsonResult("bmr data does not exist for this date");
            }
        }

        [HttpPost]
        public JsonResult CreateBMR(BMRRequest burnedCaloriesRequest)
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
