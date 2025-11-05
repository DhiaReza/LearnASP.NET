using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Bookstore_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace Bookstore_MVC
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AdminController> _logger;
        private readonly AppDbContext _context;

        private bool Locked = false;
        public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AdminController> logger, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
                var books = await _context.Book.Include(b => b.Genres).ToListAsync();

            if (!books.Any())
            {
                _logger.LogInformation("Book is Empty");
                // You could return a view that shows “No books found” or redirect elsewhere.
                return View(books);   // an empty list still satisfies the model
            }

            _logger.LogInformation($"sending {books.Count} books");
            return View(books);     // <-- passes List<Book> to the view
        }
        // admin APIs

        // get all accounts then parse to view

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ManageAccount()
        {

            var users = _userManager.Users.ToList();

            // remove current logged-in user
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (currentUser != null)
            {
                users.Remove(currentUser);

            }
            var userDtos = new List<AccountManagementDTO>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new AccountManagementDTO
                {
                    Id = user.Id,
                    Username = user.UserName ?? "Unknown User",
                    Email = user.Email ?? null,
                    PhoneNumber = user.PhoneNumber ?? null,
                    Role = roles.FirstOrDefault() ?? null
                });
            }
            return View(userDtos);
        }
        [HttpGet]
        public async Task<IActionResult> EditAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var dto = new EditAccountDTO
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = roles.FirstOrDefault() ?? "" // assume one role
                // Password fields stay empty until user fills them in
            };

            return View(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccount(EditAccountDTO account)
        {
            // Prevent admin from editing themselves
            if (User.Identity.Name == account.Username)
            {
                ModelState.AddModelError("", "Cannot edit the logged-in user.");
                return RedirectToAction("ManageAccount", "Admin");
            }

            var user = await _userManager.FindByIdAsync(account.Id.ToString());
            if (user == null)
            {
                TempData["Message"] = "User not found.";
                return RedirectToAction("ManageAccount", "Admin");
            }

            // Update basic details
            user.UserName = account.Username;
            user.Email = account.Email;
            user.PhoneNumber = account.PhoneNumber;

            // Update role
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, roles);
            }
            await _userManager.AddToRoleAsync(user, account.Role);

            // Update password (only if provided and matches confirm password)
            if (!string.IsNullOrEmpty(account.NewPassword) && !string.IsNullOrEmpty(account.ConfirmPassword))
            {
                if (account.NewPassword != account.ConfirmPassword)
                {
                    TempData["Message"] = "Passwords do not match.";
                    return RedirectToAction("ManageAccount", "Admin");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResult = await _userManager.ResetPasswordAsync(user, token, account.NewPassword);

                if (!passwordResult.Succeeded)
                {
                    TempData["Message"] = string.Join("; ", passwordResult.Errors.Select(e => e.Description));
                    return RedirectToAction("ManageAccount", "Admin");
                }
            }

            // Save user changes
            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded)
            {
                TempData["Message"] = $"Account for {user.UserName} has been updated successfully.";
            }
            else
            {
                TempData["Message"] = string.Join("; ", updateResult.Errors.Select(e => e.Description));
            }

            return RedirectToAction("ManageAccount", "Admin");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            _logger.LogInformation("recieved Id : ", id);
            if (id == null)
            {
                ModelState.AddModelError("", "No account Id");
                return RedirectToAction("ManageAccount", "Admin");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ModelState.AddModelError("", "Account not found");
                return RedirectToAction("ManageAccount", "Admin");
            }

            await _userManager.DeleteAsync(user);
            return RedirectToAction("ManageAccount", "Admin");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminChangePassword(ChangePasswordDTO password)
        {
            if (password.newPassword != password.confirmPassword)
            {
                TempData["Message"] = "Passwords do not match.";
                return RedirectToAction("ManageAccount", "Admin");
            }

            var user = await _userManager.FindByIdAsync(password.userId);
            if (user == null)
            {
                TempData["Message"] = "User not found.";
                return RedirectToAction("ManageAccount", "Admin");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, password.newPassword);

            if (result.Succeeded)
            {
                TempData["Message"] = $"Password for {user.UserName} has been changed.";
            }
            else
            {
                TempData["Message"] = string.Join("; ", result.Errors.Select(e => e.Description));
            }

            return RedirectToAction("ManageAccount", "Admin");
        }

        public IActionResult AddBook()
        {
            var bookgenre = _context.Genre.ToList();
            var genredto = new AddBookDTO
            {
                GenreNames = bookgenre
            };
            for (int i = 0; i < bookgenre.Count; i++)
            {
                _logger.LogInformation(bookgenre[i].Name);
            }
            
            
            return View(genredto);
        }

        // Tested, all possible return tested correctly
        [HttpPost]
        public async Task<ActionResult> AddBook(AddBookDTO bookdata)
        {
            if (!ModelState.IsValid)
            {
                // repopulate GenreNames before returning view
                bookdata.GenreNames = _context.Genre.ToList();
                return View(bookdata);
            }

            var book = new Book
            {
                Title = bookdata.Title,
                Author = bookdata.Author,
                Price = bookdata.Price,
                Description = bookdata.Description,
                ISBN = bookdata.ISBN,
                DateCreated = DateTime.Now,
                Genres = _context.Genre
                                .Where(g => bookdata.SelectedGenreIds.Contains(g.Id))
                                .ToList()
            };

            if (bookdata.ImageFile != null)
            {
                using var ms = new MemoryStream();
                await bookdata.ImageFile.CopyToAsync(ms);
                book.ImageData = ms.ToArray();
                book.ImageContentType = bookdata.ImageFile.ContentType;
            }

            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id)
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
            return RedirectToAction("Index");
        }

[HttpGet]
public async Task<IActionResult> EditBook(int id)
{
    var book = await _context.Book
        .Include(b => b.Genres) // load genres
        .FirstOrDefaultAsync(b => b.Id == id);

    if (book == null)
    {
        return NotFound();
    }

    var dto = new EditBookDTO
    {
        Id = book.Id,
        Title = book.Title,
        Author = book.Author,
        Price = book.Price,
        ISBN = book.ISBN,
        Stock = book.Stock,
        Description = book.Description,
        DateCreated = book.DateCreated,
        DateEdited = book.DateEdited,
        ImageData = book.ImageData,
        ImageContentType = book.ImageContentType,

        // Pre-check selected genres
        SelectedGenreIds = book.Genres.Select(g => g.Id).ToList(),

        // All available genres for checkbox list
        GenreNames = await _context.Genre.ToListAsync()
    };

    return View(dto);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> EditBook(EditBookDTO dto)
{
    if (!ModelState.IsValid)
    {
        // reload genres if invalid
        dto.GenreNames = await _context.Genre.ToListAsync();
        return View(dto);
    }

    var existingBook = await _context.Book
        .Include(b => b.Genres)
        .FirstOrDefaultAsync(b => b.Id == dto.Id);

    if (existingBook == null)
    {
        return NotFound();
    }

    // update scalar fields
    existingBook.Author = dto.Author;
    existingBook.Title = dto.Title;
    existingBook.Price = dto.Price;
    existingBook.Description = dto.Description;
    existingBook.ISBN = dto.ISBN;
    existingBook.Stock = dto.Stock;

    // handle image
    if (dto.ImageFile != null)
    {
        using var ms = new MemoryStream();
        await dto.ImageFile.CopyToAsync(ms);
        existingBook.ImageData = ms.ToArray();
        existingBook.ImageContentType = dto.ImageFile.ContentType;
    }

    // update genres
    existingBook.Genres.Clear(); // remove old ones
    if (dto.SelectedGenreIds != null && dto.SelectedGenreIds.Any())
    {
        var selectedGenres = await _context.Genre
            .Where(g => dto.SelectedGenreIds.Contains(g.Id))
            .ToListAsync();

        foreach (var genre in selectedGenres)
        {
            existingBook.Genres.Add(genre);
        }
    }

    existingBook.DateEdited = DateTime.Now;

    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
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
        [HttpGet]
        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddGenre(GenreDTO dto)
        {
            if (ModelState.IsValid)
            {
                var genre = new BookGenre // Assuming you have a Genre entity
                {
                    Name = dto.Name
                };

                _context.Genre.Add(genre);
                _context.SaveChanges();
                return RedirectToAction("Index"); // Redirect to list of genres
            }

            return View(dto);
        }
    }
}