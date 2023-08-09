using Microsoft.AspNetCore.Mvc;

namespace Lab_Routing_Navigation.Areas.admin.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            return View();
        }
    }
}
