using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Web_API_NEW.Data.Helpers;

namespace School_Web_API_NEW.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles =UserRoles.Student+","+UserRoles.Manager)]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
            
        }

        [HttpGet("student")]
        [Authorize(Roles = UserRoles.Student )]
        public IActionResult GetStudent() 
        {
            return Ok("Welcome to Home Controller called by student");
        }

        [HttpGet("manager")]
        [Authorize(Roles = UserRoles.Manager)]
        public IActionResult GetManager()
        {
            return Ok("Welcome to Home Controller called by manager");
        }
    }
}
