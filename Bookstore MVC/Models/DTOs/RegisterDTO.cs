using System.ComponentModel.DataAnnotations;

namespace Bookstore_MVC.Models
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Username must be at least 4 characters long.")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        public string? Role { get; set; }

        public bool Rememberme { get; set; }

        public RegisterDTO()
        {
            Role = "Customer";
        }
    }
}