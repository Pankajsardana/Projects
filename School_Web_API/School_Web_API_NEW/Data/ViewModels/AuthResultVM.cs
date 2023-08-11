namespace School_Web_API_NEW.Data.ViewModels
{
    public class AuthResultVM
    {
     
        public string Token  { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }

    }
}
