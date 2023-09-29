using HealthKoppeling.Interfaces;
using HealthKoppeling.Models;
using HealthKoppeling.Requests;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthKoppeling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IManager<UserModel> userManager;

        public UserController(IManager<UserModel> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public JsonResult CreateUser(UserRequest user)
        {
            UserModel newUser = new UserModel(user.Name, user.Email, user.AccessToken);
            if (userManager.CheckIfExists(newUser))
            {
                return new JsonResult("user exists");
            }
            else
            {
                userManager.Add(newUser);
                return new JsonResult(Ok());
            }
        }
    }
}
