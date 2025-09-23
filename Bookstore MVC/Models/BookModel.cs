using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookstore_MVC.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Book title is required!")]
        [StringLength(100, ErrorMessage = "The Title cannot exceed 100 characters. ")]
        
        public string Title { get; set; }
        [Required(ErrorMessage = "Book author is required!")]
        [StringLength(100, ErrorMessage = "The Author Name cannot exceed 100 characters. ")]

        public string Author { get; set; }
        [Required(ErrorMessage = "Please enter a price")]
        [Range(0.0, 999999.99)]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        //[Required(ErrorMessage = "Please fill book genres")]
        public ICollection<BookGenre> Genres { get; set; } = new List<BookGenre>();
        
        [Required(ErrorMessage = "Please fill book ISBN")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "Please fill book stock")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "Please fill book description")]
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateEdited { get; set; }

        public byte[]? ImageData { get; set; }
        public string? ImageContentType { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}