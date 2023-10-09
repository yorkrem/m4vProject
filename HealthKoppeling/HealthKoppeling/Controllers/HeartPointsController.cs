using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;
using HealthKoppeling.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthKoppeling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartPointsController : ControllerBase
    {
        private IManager<HeartPointModel> heartPointsManager;

        public HeartPointsController(IManager<HeartPointModel> manager)
        {
            this.heartPointsManager = manager;  
        }

        [HttpPost]
        public JsonResult createHealth(HeartPointRequest heartPointRequest)
        {
            HeartPointModel heartPointModel = new HeartPointModel(heartPointRequest.HeartPoints, heartPointRequest.StartTime, heartPointRequest.EndTime, heartPointRequest.UserEmail);
            if (heartPointsManager.CheckIfExists(heartPointModel))
            {
                heartPointsManager.Update(heartPointModel);
                return new JsonResult("heart point exists");
            }
            else
            {
                heartPointsManager.Add(heartPointModel);
                return new JsonResult("heart point added");
            }
        }
    }
}
