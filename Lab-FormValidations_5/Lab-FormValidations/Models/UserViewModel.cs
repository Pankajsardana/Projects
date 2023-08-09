using Lab_FormValidations.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Lab_FormValidations.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage="Please enter the Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the Email")]
        public string  Email { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Password required (1 Uppercase, 1 Number, 1 Special Char and 1 Lowercase Char) with Min Length 8 Chars")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Please Enter Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm Password doesn't match")]
        public string ConfirmPassword { get; set; }

        [RegularExpression("^[6789]\\d{9}$", ErrorMessage = "Please Enter Correct Mobile No")] 
        public string? Contact { get; set; }
        
        [Required(ErrorMessage = "Gender is required")] 
        public string Gender { get; set; }

        [ValidateCheckBox(ErrorMessage = "Please Accept Terms")]
        public bool Terms { get; set; }


    }
}
