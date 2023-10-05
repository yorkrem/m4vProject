using HealthKoppeling.Interfaces;
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

        public BMRController(IManager<BMRModel> bmrManager)
        {
            this.bmrManager = bmrManager;
        }

        [HttpPost]
        public JsonResult createBMR(BMRRequest bMRRequest)
        {
            BMRModel bmr = new BMRModel(bMRRequest.Bmr, bMRRequest.EndTime, bMRRequest.UserEmail);
            if(bmrManager.CheckIfExists(bmr))
            {
                bmrManager.Update(bmr);
                return new JsonResult("bmr updated");
            }
            else
            {
                bmrManager.Add(bmr);
                return new JsonResult(Ok());
            }
        }

        
    }
}
