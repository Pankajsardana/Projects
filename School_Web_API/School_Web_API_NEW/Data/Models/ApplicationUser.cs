using Microsoft.AspNetCore.Identity;

namespace School_Web_API_NEW.Data.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get;set; }
        public string LastName { get;set; }

        public string Custom { get;set; }=string.Empty;

    }
}
