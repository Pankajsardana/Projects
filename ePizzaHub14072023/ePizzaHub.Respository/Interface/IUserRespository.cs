using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Respository.Interface
{
    public interface IUserRespository
    {
        bool CreateUser(User user, string Role);
        bool ValidateEmail(string email);
        UserModel ValidateUser(string username, string password);
    }
}
