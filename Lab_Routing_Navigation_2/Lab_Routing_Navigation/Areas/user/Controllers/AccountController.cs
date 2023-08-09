using Microsoft.AspNetCore.Mvc;

namespace Lab_Routing_Navigation.Areas.user.Controllers
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
