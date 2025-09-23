using System.ComponentModel.DataAnnotations;

namespace Bookstore_MVC
{
    public class ChangePasswordDTO
    {
        public string userId { get; set; }
        [Required(ErrorMessage = "New Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string newPassword { get; set; }
        [Required(ErrorMessage = "Confirm New Password")]
        public string confirmPassword { get; set; }
    }
}

