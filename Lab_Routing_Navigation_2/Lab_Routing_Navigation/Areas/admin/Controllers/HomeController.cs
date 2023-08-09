using Microsoft.AspNetCore.Mvc;

namespace Lab_Routing_Navigation.Areas.admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
