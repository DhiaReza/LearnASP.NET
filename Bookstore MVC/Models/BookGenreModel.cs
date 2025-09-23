using System.ComponentModel.DataAnnotations;

namespace Bookstore_MVC.Models
{
    public class BookGenre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}