using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using m4vAPI.Models;

namespace m4vAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public JsonResult CreateUser(UserModel user)
        {
            return new JsonResult("");
        }
    }
}
