using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Web_API_NEW.Data.Helpers;

namespace School_Web_API_NEW.Controllers
{
    [Authorize(Roles = UserRoles.Student)]
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
