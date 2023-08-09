using BCrypt.Net;
using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Respository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Respository.Implementation
{
    public class UserRespository :Respository<User>, IUserRespository 
    {
        public UserRespository(AppDbContext db) : base(db)
        {

        }

        public bool CreateUser(User user, string Role)
        {
            try 
            {
                Role role = _db.Roles.Where(r=>r.Name == Role).FirstOrDefault();

                if (role != null) 
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                    user.Roles.Add(role);

                    _db.Users.Add(user);

                    _db.SaveChanges();
                    return true;    
                }
            }
            catch(Exception ex) 
            {
                return false;
            }
            return false;
        }

        public bool ValidateEmail(string email)
        {
           User user=_db.Users.FirstOrDefault(r => r.Email == email);

            if (user != null) 
            {
                return false;
            }
            else { return true; }
        }

        public UserModel ValidateUser(string email, string password)
        {
            User user=_db.Users.Include(u=>u.Roles).Where(u=>u.Email== email).FirstOrDefault();
            if (user!=null)
            {
                bool isVerified = BCrypt.Net.BCrypt.Verify(password, user.Password);
                if (isVerified) 
                {
                    return new UserModel 
                    { 
                        Email = user.Email, 
                        Password = user.Password,
                        Name=user.Name,
                        PhoneNumber=user.PhoneNumber,
                        Id=user.Id,
                        Roles=user.Roles.Select(r=>r.Name).ToArray(),
                    };
                }
            }
            return null;
        }
    }
}
