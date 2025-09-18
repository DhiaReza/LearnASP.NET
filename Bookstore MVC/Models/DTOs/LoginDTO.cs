using System.ComponentModel.DataAnnotations;

namespace Bookstore_MVC.Models
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Please fill in your Username")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Please fill in your Password.")]
        public string Password { get; set; }
        public bool Rememberme { get; set; }
    }
}