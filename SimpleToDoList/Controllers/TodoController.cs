using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimpleToDoList.Models;

namespace SimpleToDoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly ILogger<TodoController> _logger;
        private readonly AppDbContext _context;

        public TodoController(ILogger<TodoController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "THIS IS A SIMPLE TASK LIST";
            var todoitem = _context.ToDoName.ToList();
            //foreach (var item in todoitem)
            //{
            //    _logger.LogInformation("ToDoItem - Id: {Id}, Name: {Name}, Description: {Description}, Status: {Status}, DateCreated: {DateCreated}",
            //        item.ToDoId, item.Name, item.Description, item.IsComplete, item.DateCreated);
            //}
            return View(todoitem);
        }
        [HttpPost]
        public IActionResult CreateItem([FromBody] ToDoItem item)
        {
            _logger.LogInformation("Received item - Name: {Name}, Description: {Description}", item.Name, item.Description);

            if (ModelState.IsValid && 
                !string.IsNullOrWhiteSpace(item.Name) && 
                !string.IsNullOrWhiteSpace(item.Description))
            {
                _context.ToDoName.Add(item);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest("Name and Description are both required. Please fill out both fields.");
            }
        }
        [HttpGet]
        public IActionResult EditView(int id)
        {
            var todoitem = _context.ToDoName.Find(id);
            if (todoitem != null)
            {
                return View(todoitem);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        public IActionResult EditItem(int id)
        {
            // edit logic here
            return View();
        }
        [HttpDelete]
        public IActionResult DeleteItem(int id)
        {
            var todoitem = _context.ToDoName.Find(id);
            if (todoitem == null)
            {
                return NotFound();
            }

            _context.ToDoName.Remove(todoitem);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPut]
        public IActionResult ChangeStatus(int id)
        {
            var todoitem = _context.ToDoName.Find(id);
            if (todoitem.IsComplete == true)
            {
                todoitem.IsComplete = false;
            }
            else
            {
                todoitem.IsComplete = true;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult GetItem(int id)
        {
            var todoitem = _context.ToDoName.Find(id);
            if (todoitem == null)
            {
                return NotFound();
            }
            else
            {
                return View(todoitem);
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
