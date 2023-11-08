using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;
using HealthKoppeling.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthKoppeling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoveMinutesController : ControllerBase
    {
        private IManager<MoveMinutesModel> moveMinutesManager;

        public MoveMinutesController(IManager<MoveMinutesModel> manager) 
        {
            this.moveMinutesManager = manager;
        }

        [HttpPost]
        public JsonResult createMoveMinutes(MoveMinutesRequest moveMinutesRequest)
        {
            MoveMinutesModel moveMinutesModel = new MoveMinutesModel(moveMinutesRequest.MoveMinutes, moveMinutesRequest.StartTime, moveMinutesRequest.EndTime, moveMinutesRequest.UserEmail);
            if (moveMinutesManager.CheckIfExists(moveMinutesModel))
            {
                moveMinutesManager.Update(moveMinutesModel);
                return new JsonResult("moveMinutes updated");
            }
            else
            {
                moveMinutesManager.Add(moveMinutesModel);
                return new JsonResult(Ok());
            }
        }
    }
}
