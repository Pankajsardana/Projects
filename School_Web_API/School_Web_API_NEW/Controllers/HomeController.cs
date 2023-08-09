using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace School_Web_API_NEW.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
            
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("Welcome to Home Controller");
        }
    }
}
