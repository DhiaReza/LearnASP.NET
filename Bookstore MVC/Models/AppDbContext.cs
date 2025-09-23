using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bookstore_MVC.Models
{
    // ApplicationDbContext includes both Identity and your app tables
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<BookGenre> Genre { get; set; }
        public DbSet<Book> Book { get; set; }

        [Required(ErrorMessage = "Please add date created")]
        public DateTime DateCreated { get; set; }
    }
}