using Lab_DataPassing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Lab_DataPassing.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(model.Username=="user@gmail.com" && model.Password == "12345678") 
                {
                    TempData["Message"] = "Welcome Back";
                    string struser=JsonSerializer.Serialize(model);
                    HttpContext.Session.SetString("User", struser);
                    return RedirectToAction("Index", "Dashboard");
                }
                else 
                {
                    ViewBag.ErrorMessage = "Usernmae or password does not exist!";
                }


            }
            return View();
        }
    }
}
