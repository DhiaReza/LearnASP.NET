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
    public IActionResult Add()
    {
        return View();
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

    // Tested, all possible return tested correctly
    [HttpPost]
    public async Task<ActionResult> Add(Book bookdata)
    {
        if (!ModelState.IsValid)
        {
            foreach (var entry in ModelState.Where(e => e.Value.Errors.Count > 0))
            {
                foreach (var error in entry.Value.Errors)
                {
                    _logger.LogWarning("Validation error on field '{Field}': {ErrorMessage}", entry.Key, error.ErrorMessage);
                }
            }
            return View(bookdata);
        }
        if (bookdata.ImageFile != null)
        {
            using var ms = new MemoryStream();
            await bookdata.ImageFile.CopyToAsync(ms);
            bookdata.ImageData = ms.ToArray();
            bookdata.ImageContentType = bookdata.ImageFile.ContentType;
        }
        _logger.LogInformation($"Add book :\n{bookdata.Title}\n{bookdata.Author}\n{bookdata.Price}");
        _context.Add(bookdata);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");;
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
    // GET: Book/Edit/27
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.Book.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

// POST: Book/Edit/27
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(Book book)
{
    if (!ModelState.IsValid)
    {
        return View(book);
    }

    var existingBook = await _context.Book.FindAsync(book.Id);
    if (existingBook == null)
    {
        return NotFound();
    }

    existingBook.Author = book.Author;
    existingBook.Title = book.Title;
    existingBook.Price = book.Price;
    existingBook.Description = book.Description;
    existingBook.ImageData = book.ImageData;

    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
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
