using System.ComponentModel.DataAnnotations;

namespace Bookstore_MVC.Models
{
    public class AccountDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Rememberme { get; set; }
    }
}