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
    public async Task<IActionResult> Index(string sortOrder)
    {

        ViewBag.NameSortParam = sortOrder == "name_asc" ? "name_desc" : "name_asc";
        ViewBag.DateSortParam = sortOrder == "date_asc" ? "date_desc" : "date_asc";
        
        var books = from book in _context.Book select book;

        if (!books.Any())
        {
            _logger.LogInformation("Book is Empty");
            // You could return a view that shows “No books found” or redirect elsewhere.
            return View(Enumerable.Empty<Book>());   // an empty list still satisfies the model
        }

        switch (sortOrder)
        {
            case "name_desc":
                books = books.OrderByDescending(u => u.Title);
                break;
            case "date_asc":
                books = books.OrderBy(u => u.DateCreated);
                break;
            case "date_desc":
                books = books.OrderByDescending(u => u.DateCreated);
                break;
            default:
                books = books.OrderBy(u => u.Title); // default ascending
                break;
        }
        await books.ToListAsync();
        _logger.LogInformation($"sending {books.Count()} books");

        return View(books);     // <-- passes List<Book> to the view
    }
    [HttpGet]
    public IActionResult Details([FromRoute] int id)
    {
        var book = BookDetails(id);
        _logger.LogInformation($"Opening book details of {book}");
        return View(book);
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
}
