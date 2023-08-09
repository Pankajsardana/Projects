using Microsoft.AspNetCore.Mvc;

namespace School_Web_API_NEW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        public ManagementController()
        {
            
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welecome to Management Controller");
        }


    }
}
