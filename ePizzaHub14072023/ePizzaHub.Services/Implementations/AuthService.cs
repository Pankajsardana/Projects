using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Respository.Implementation;
using ePizzaHub.Respository.Interface;
using ePizzaHub.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Implementations
{
    public class AuthService:IAuthService
    {
        IUserRespository _userRespo;
        public AuthService(IUserRespository userRespository) 
        {
            _userRespo = userRespository;
        }

        public bool CreateUser(User user, string Role)
        {
           return _userRespo.CreateUser(user, Role);
        }

        public bool VaildateEmail(string email)
        {
            return _userRespo.ValidateEmail(email);
        }

        public UserModel ValidateUser(string username, string password)
        {
            return _userRespo.ValidateUser(username, password);
        }
    }
}
