using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Interface
{
    public interface IAuthService
    {
        bool CreateUser(User user, string Role);
        UserModel ValidateUser(string email, string password);

        bool VaildateEmail(string email);

    }
}
