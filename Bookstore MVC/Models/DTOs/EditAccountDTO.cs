using System.ComponentModel.DataAnnotations;

namespace Bookstore_MVC.Models
{
    public class EditAccountDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Username must be at least 4 characters long.")]
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public string Role { get; set; }

        [Required(ErrorMessage = "New Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password")]
        public string ConfirmPassword { get; set; }
    }

}