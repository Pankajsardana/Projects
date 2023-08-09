using Microsoft.AspNetCore.Mvc;

namespace Lab_DataPassing.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
