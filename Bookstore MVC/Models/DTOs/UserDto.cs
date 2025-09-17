using System.ComponentModel.DataAnnotations;

namespace Bookstore_MVC.Models
{
    public class UserDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Username must be at least 4 characters long.")]
        public string username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string password { get; set; }
        public bool rememberme { get; set; }
    }
}