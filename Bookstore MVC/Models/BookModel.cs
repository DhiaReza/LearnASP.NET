using System.ComponentModel.DataAnnotations;

namespace Bookstore_MVC.Models
{
    public class Book
    {
        [Required][Key]
        public int Id { get; set; }
        [Required][StringLength(300, ErrorMessage = "The Title cannot exceed 300 characters. ")] 
        public string Title { get; set; }
        [Required][StringLength(100, ErrorMessage = "The Author Name cannot exceed 100 characters. ")] 
        public string Author { get; set; }
        [Required][Range(0,1000, ErrorMessage = "The Price for The Book must be between {1} and {1000}.")]
        public decimal Price { get; set; }
    }
}