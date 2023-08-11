using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using School_Web_API_NEW.Data.Helpers;

namespace School_Web_API_NEW.Controllers
{

    [Authorize(Roles=UserRoles.Manager)]
   [Route("[controller]")]
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
