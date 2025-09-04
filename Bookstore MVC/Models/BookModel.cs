using System.ComponentModel.DataAnnotations;

namespace Bookstore_MVC.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Book title is required!")]
        [StringLength(100, ErrorMessage = "The Title cannot exceed 100 characters. ")] 
        public string Title { get; set; }
        [Required(ErrorMessage = "Book author is required!")]
        [StringLength(100, ErrorMessage = "The Author Name cannot exceed 100 characters. ")] 
        public string Author { get; set; }
        [Required(ErrorMessage = "Book price is required!")]
        [Range(0, 1000, ErrorMessage = "The Price for {0} must be between {1} and {2}.")]
        public int Price { get; set; }
    }
}