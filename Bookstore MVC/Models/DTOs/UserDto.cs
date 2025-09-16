using System.ComponentModel.DataAnnotations;

namespace Bookstore_MVC.Models
{
    public class UserDto
    {
        [Required]
        public string username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        public bool rememberme { get; set; }
    }
}