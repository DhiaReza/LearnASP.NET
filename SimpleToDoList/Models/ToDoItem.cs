using System.ComponentModel.DataAnnotations;

namespace SimpleToDoList.Models
{
    public class ToDoItem
    {
        [Key]
        public int ToDoId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public DateTime DateCreated { get; set; }

        public ToDoItem()
        {
            IsComplete = false;
            DateCreated = DateTime.Now;
        }
    }
}
