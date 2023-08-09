using Microsoft.AspNetCore.Mvc;

namespace School_Web_API_NEW.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        public StudentController()
        {
            
        }

        [HttpGet]
        public IActionResult Get() 
        {
            return Ok("Welcome to Student Controller");
        }
    }
}
