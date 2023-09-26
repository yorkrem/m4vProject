using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using m4vAPI.Managers;
using m4vAPI.Requests;
using m4vAPI.Models;
using m4vAPI.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace m4vAPI.Controllers
{
    [EnableCors("*")]
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IManager<UserModel> userManager;

        public UserController(IManager<UserModel> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public JsonResult CreateUser(userRequest user)
        {
            UserModel newUser = new UserModel(user.name, user.email, user.accessToken);
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
