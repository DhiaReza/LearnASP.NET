using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bookstore_MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore_MVC
{
    public class GenreDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please add book genre")]
        public string Name { get; set; }
        public ICollection<Book> Books{ get; set; } = new List<Book>();
    }
}