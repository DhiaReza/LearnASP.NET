using Microsoft.EntityFrameworkCore;

namespace SimpleToDoList.Models
{
    public class AppDbContext : DbContext
    {

        public DbSet<ToDoItem> ToDoName { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    };
}
