using System.ComponentModel.DataAnnotations;

namespace School_Web_API_NEW.Data.ViewModels
{
    public class LoginVM
    {

        [Required]
        public string EmailAddress { get; set; }
        [Required]
      
        public string Password { get; set; }
    }
}
