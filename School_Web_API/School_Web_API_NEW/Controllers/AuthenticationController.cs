using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using School_Web_API_NEW.Data.Models;
using School_Web_API_NEW.Data.ViewModels;
using School_Web_API_NEW.Data;
using School_Web_API_NEW.Data;
using School_Web_API_NEW.Data.Models;
using School_Web_API_NEW.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace School_Web_API_NEW.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register-user")]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all the required fields");
            }

            var userExists = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (userExists != null)
            {
                return BadRequest($"User {registerVM.EmailAddress} already exists");
            }

            ApplicationUser newUser = new ApplicationUser()
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (result.Succeeded) return Ok("User created");
            return BadRequest("User could not be created");
        }
        [HttpPost("login-user")]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest("Please provide all the details");
            }
            var userExists = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if(userExists!= null && await _userManager.CheckPasswordAsync(userExists,loginVM.Password))
            {
                var token = await GenerateJWTTokenAsync(userExists);


                return Ok(token);
            }
            return Unauthorized();
        }

        private async Task<AuthResultVM> GenerateJWTTokenAsync(ApplicationUser user)
        {
            var authClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                

            };
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(5),
                claims: authClaim,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            

            var SecurityToken=new JwtSecurityTokenHandler().WriteToken(token);
            var response = new AuthResultVM
            {
                Token = SecurityToken,
                ExpiresAt = token.ValidTo

            };
            return response;
        }
    }
}
