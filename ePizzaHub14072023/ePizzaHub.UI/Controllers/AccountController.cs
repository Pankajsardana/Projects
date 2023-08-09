using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Services.Interface;
using ePizzaHub.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text.Json;

namespace ePizzaHub.UI.Controllers
{
    public class AccountController : Controller
    {
        IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {

                // _authService.VaildateEmail(loginViewModel.Email)
                UserModel user = _authService.ValidateUser(loginViewModel.Email, loginViewModel.Password);

                if (user != null)
                {
                    GenerateTicket(user);
                    if (user.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else if (user.Roles.Contains("User"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "User" });
                    }
                }
                else
                {
                    ViewBag.Message = "Username or Password does not exist!";
                }
            }
            return View();
        }

        void GenerateTicket(UserModel user) 
        {
            string strData=JsonSerializer.Serialize(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.UserData, strData),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, string.Join(",",user.Roles))
            };

            var identity=new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(identity),
                
                new AuthenticationProperties 
                {
                    AllowRefresh=true,
                    ExpiresUtc=DateTime.UtcNow.AddMinutes(60),             
                
                });
        }
        
        public IActionResult LogOut() 
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult unauthorize()
        {
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(UserViewModel userViewModel)
        {
            if (ModelState.IsValid) 
            {
               // UserModel data=_authService.ValidateUser(userViewModel.Email,userViewModel.Password);

                if (!_authService.VaildateEmail(userViewModel.Email)) 
                {
                    User user = new User
                    {
                        Name=userViewModel.Name,
                        Email=userViewModel.Email,
                        CreatedDate=DateTime.Now,
                        Password=userViewModel.Password,
                        PhoneNumber=userViewModel.PhoneNumber
                    };
                    bool result = _authService.CreateUser(user, "User");
                    if (result) 
                    {
                        return RedirectToAction("Login");
                    }



                }
                else 
                {
                    ViewBag.Message = "Email id is already exist";
                    return View();
                }

                return View(userViewModel);
            }

            return View();
        }
    }
}
