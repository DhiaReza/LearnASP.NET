using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookstore_MVC.Models;

namespace Bookstore_MVC.Controllers;
[ApiController]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var books = _context.Book.ToList();
        return View(books);
    }
    [HttpPost]
    public async Task<ActionResult> AddBook([FromBody] Book bookdata)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Model is not valid");
            var book = _context.Book.ToList();
            return View("Index", book);
        }
        _context.Add(bookdata);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<ActionResult> BookDetails([FromRoute] int id)
    {
        if (id == null)
        {
            _logger.LogWarning("Id is not provided");
            return BadRequest("No Id Provided");
        }
        var bookdetails = await _context.Book.FindAsync(id);
        if (bookdetails == null)
        {
            _logger.LogInformation($"Book with Id of : {id} is not found");
            return NotFound("No book found");
        }
        _logger.LogInformation($"Book been found\nBook Tittle : {bookdetails.Title}\nBook Author: {bookdetails.Author}\nBook Price: {bookdetails.Price}");
        return View(bookdetails);
    }
    [HttpPut]
    public async Task<ActionResult> EditBook([FromBody] Book book)
    {
        if (book == null)
        {
            _logger.LogWarning("Id is not provided");
            return BadRequest("No Id Provided");
        }
        var bookdetails = await _context.Book.FindAsync(book.Id);
        if (bookdetails == null)
        {
            _logger.LogInformation($"Book with Id of : {book.Id} is not found");
            return NotFound("No book found");
        }
        bookdetails.Author = book.Author;
        bookdetails.Title = book.Title;
        bookdetails.Price = book.Price;

        await _context.SaveChangesAsync();

        return View();
    }
    [HttpDelete]
    public async Task<ActionResult> DeleteBook([FromRoute] int id)
    {
        if (id == null)
        {
            _logger.LogWarning("Id is not provided");
            return BadRequest("No Id Provided");
        }
        var bookdetails = await _context.Book.FindAsync(id);
        if (bookdetails == null)
        {
            _logger.LogInformation($"Book with Id of : {id} is not found");
            return NotFound("No book found");
        }
        _context.Book.Remove(bookdetails);
        await _context.SaveChangesAsync();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
