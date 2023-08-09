using System.ComponentModel.DataAnnotations;

namespace ePizzaHub.UI.Models
{
    public class UserViewModel
    {

        [Required(ErrorMessage ="Please enter the Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter the Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please confirm the Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please enter the Phone Number")]
        public string PhoneNumber { get; set; }


    }
}
