using System.ComponentModel.DataAnnotations;

namespace School_Web_API_NEW.Data.ViewModels
{
    public class TokenRequestVM
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; } = string.Empty;

    }
}
