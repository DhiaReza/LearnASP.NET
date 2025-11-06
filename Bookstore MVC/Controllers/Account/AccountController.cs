using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Bookstore_MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Microsoft.EntityFrameworkCore;


namespace Bookstore_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly AppDbContext _context;

        private bool Locked = false;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }
        // roles :
        // admin
        // staff
        // customer

        //login for both admin/staff and user

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            var username = User.Identity.Name;
            if (username != null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid login attempt");  
                return View(user);
            }
            var result = await _signInManager.PasswordSignInAsync(user.Username, user.Password, user.Rememberme, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(user.Username);
                var currentRole = await _userManager.GetRolesAsync(currentUser);
                var role = currentRole.FirstOrDefault();
                switch (role)
                {
                    case "Admin":
                        _logger.LogInformation($"User {user.Username} logged in as Admin.");
                        return RedirectToAction("Index", "Admin");
                    case "staff":
                        _logger.LogInformation($"User {user.Username} logged in as Staff.");
                        return RedirectToAction("Index", "Staff");
                    case "Customer":
                        _logger.LogInformation($"User {user.Username} logged in as Customer.");
                        return RedirectToAction("Index", "Book");
                    default:
                        break;
                }
            }

            // Sign‑in failed – add a model‑level error.
            ModelState.AddModelError("", "Invalid login attempt");
            return View(user);
        }

        // logout
        public async Task<IActionResult> Logout()
        {
            var username = User.Identity.Name;
            if (username == null)
            {
                _logger.LogInformation($"No user is logged in");
                return BadRequest();
            }
            else
            {
                _logger.LogInformation($"Logged out: {username}");
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Book");
            }
        }

        // Register Customer

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO user)
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        _logger.LogError($"Key: {state.Key}, Error: {error.ErrorMessage}");
                    }
                }
                _logger.LogInformation($"Model is not valid, {user.Username} {user.Password}");
                return View(user);
            }


            var newUser = new IdentityUser { UserName = user.Username };
            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                _logger.LogInformation("An error occured");
                return View(user);
            }

            await _userManager.AddToRoleAsync(newUser, user.Role);
            _logger.LogInformation($"Created user with {user.Username} name");
            if (User.IsInRole("Admin") != true)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: true);
            }
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("ManageAccount", "Admin");
            }
            return RedirectToAction("Index", "Book");
        }


    }
}
