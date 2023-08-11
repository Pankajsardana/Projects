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
using Microsoft.EntityFrameworkCore;
using School_Web_API_NEW.Data.Helpers;

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

        private readonly TokenValidationParameters _tokenValidationParameters;

        public AuthenticationController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            AppDbContext context,
            IConfiguration configuration,
            TokenValidationParameters tokenValidationParameters)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
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

            if (result.Succeeded) {
                //Add the user Role

                switch (registerVM.Role) 
                {
                    case UserRoles.Manager:
                        await _userManager.AddToRoleAsync(newUser,UserRoles.Manager);
                        break;

                    case UserRoles.Student:
                        await _userManager.AddToRoleAsync(newUser, UserRoles.Student);
                        break;
                    default:
                        break;


                }

                return Ok("User created"); 
            }
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
                var token = await GenerateJWTTokenAsync(userExists,null);


                return Ok(token);
            }
            return Unauthorized();
        }

        private async Task<AuthResultVM> GenerateJWTTokenAsync(ApplicationUser user, RefreshToken rToken)
        {
            var authClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                

            };
            //Add user Role claims

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach(var role in userRoles) 
            {
                authClaim.Add(new Claim(ClaimTypes.Role, role));
            }


            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.UtcNow.AddMinutes(5),
                claims: authClaim,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));
            

            var SecurityToken=new JwtSecurityTokenHandler().WriteToken(token);

            if(rToken != null) 
            {
                var rTokenReponse = new AuthResultVM()
                {
                    Token=SecurityToken,
                    RefreshToken=rToken.Token,
                    ExpiresAt=token.ValidTo
                };
                return rTokenReponse;
            }

            var refreshToken = new RefreshToken()
            {
                JWTID=token.Id,
                IsRevoked=false,
                UserId=user.Id,
                DateAdded=DateTime.UtcNow,
                DateExpired=DateTime.UtcNow.AddMonths(6),
                Token=Guid.NewGuid().ToString()+"-"+Guid.NewGuid().ToString(),

            };
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            var response = new AuthResultVM
            {
                Token = SecurityToken,
                RefreshToken=refreshToken.Token,
                ExpiresAt = token.ValidTo

            };
            return response;
        }


        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestVM tokenRefreshVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all the details");
            }
            var result = await VerifyandGenerateToken(tokenRefreshVM);
            return Ok(result);
        }

        private async Task<AuthResultVM> VerifyandGenerateToken(TokenRequestVM tokenRefreshVM)
        {
           var jwtSecurityTokenHandler =new JwtSecurityTokenHandler();
            var storedToken=await _context.RefreshTokens.FirstOrDefaultAsync
                (x=>x.Token==tokenRefreshVM.RefreshToken);

            var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
            try 
            {
                var tokenCheckResult = jwtSecurityTokenHandler.ValidateToken
                    (tokenRefreshVM.Token, _tokenValidationParameters, out var validatedToken);
                return await GenerateJWTTokenAsync(dbUser, storedToken);

            }
            catch (SecurityTokenException ex) 
            {
                if (storedToken.DateExpired >= DateTime.UtcNow) 
                {
                    return await  GenerateJWTTokenAsync(dbUser, storedToken);
                }
                else
                {
                    return await GenerateJWTTokenAsync(dbUser, null);
                }
            }
            
        }
    }
}
