using System.ComponentModel.DataAnnotations;

namespace WebApplication.Model.Auth
{
    public class Login
    {
        [Required]
        [EmailAddress]
        public string Mail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
