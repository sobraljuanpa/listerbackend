using ListerAPI.Models;
using ListerAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ListerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController
    {
        private IUserService service;
        public UsersController(IUserService userService)
        {
            service = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> GetUsers()
        {
            return service.GetUsers();
        }
    }
}
