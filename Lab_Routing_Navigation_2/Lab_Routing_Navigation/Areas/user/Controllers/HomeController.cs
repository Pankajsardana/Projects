using Microsoft.AspNetCore.Mvc;

namespace Lab_Routing_Navigation.Areas.user.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
