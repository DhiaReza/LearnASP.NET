using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Bookstore_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Bookstore_MVC.Migrations;

namespace Bookstore_MVC.Controllers;
public class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly AppDbContext _context;

    public BookController(ILogger<BookController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var books = await _context.Book.ToListAsync();

        if (!books.Any())
        {
            _logger.LogInformation("Book is Empty");
            // You could return a view that shows “No books found” or redirect elsewhere.
            return View(Enumerable.Empty<Book>());   // an empty list still satisfies the model
        }

        _logger.LogInformation($"sending {books.Count} books");
        return View(books);     // <-- passes List<Book> to the view
    }
    [HttpGet]
    public IActionResult Details([FromRoute] int id)
    {
        var book = BookDetails(id);
        _logger.LogInformation($"Opening book details of {book}");
        return View(book);
    }


    // Tested, returns all books listed, return emprty if empty
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _context.Book.ToListAsync();
        if (books.Count == 0)
        {
            _logger.LogInformation("Book is Empty");
            return Ok("Book Is Empty");
        }
        _logger.LogInformation($"sending {books.Count} books");
        return Ok(books);
    }


    public IActionResult GetImage(int id)
    {
        var product = _context.Book.Find(id);
        if (product == null || product.ImageData == null)
        {
            return NotFound();
        }

        return File(product.ImageData, product.ImageContentType ?? "application/octet-stream");
    }

    //tested, returns just 1 book by Id
    [HttpGet]
    public async Task<ActionResult> BookDetails(int id)
    {
        if (id == null)
        {
            _logger.LogWarning("Id is not provided");
            return BadRequest("No Id Provided");
        }
        var bookdetails = await _context.Book.FindAsync(id);
        if (bookdetails == null)
        {
            _logger.LogInformation($"Book with Id of {id} is not found");
            return NotFound($"Book with Id of {id} is not found");
        }
        _logger.LogInformation($"Book been found\nBook Tittle : {bookdetails.Title}\nBook Author: {bookdetails.Author}\nBook Price: {bookdetails.Price}");
        return Ok(bookdetails);
    }


    //tested, delete book by id
    [HttpDelete]
    public async Task<ActionResult> DeleteBook([FromRoute] int? id)
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
        _logger.LogInformation($"{bookdetails.Title} deleted");
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
