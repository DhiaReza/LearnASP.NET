using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Bookstore_MVC.Models;
using Microsoft.Identity.Client;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;
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

        // Customer Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO user)
        {
            var username = User.Identity.Name;
            if (username != null)
            {
                return View(user);
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }


            var newUser = new IdentityUser { UserName = user.Username };
            var result = await _userManager.CreateAsync(newUser, user.Username);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(user);
            }

            await _userManager.AddToRoleAsync(newUser, "Customer");
            _logger.LogInformation($"Created user with {user.Username} name");

            await _signInManager.SignInAsync(newUser, isPersistent: true);
            return RedirectToAction("Index", "Book");
        }

        // Staff Register
        [Authorize("Admin")]
        [HttpGet]
        public IActionResult RegisterStaff()
        {
            return View();
        }

        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> RegisterStaff(RegisterDTO user)
        {
            var username = User.Identity.Name;
            if (username != null)
            {
                return View(user);
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }


            var newUser = new IdentityUser { UserName = user.Username };
            var result = await _userManager.CreateAsync(newUser, user.Username);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(user);
            }

            await _userManager.AddToRoleAsync(newUser, "Staff");
            _logger.LogInformation($"Created user with {user.Username} name");

            await _signInManager.SignInAsync(newUser, isPersistent: true);
            return RedirectToAction("Management", "Account");
        }

        //login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
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
                _logger.LogInformation($"User {user.Username} logged in.");
                return RedirectToAction("Index", "Book");
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

        // get all accounts then parse to view

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Manage()
        {
            var users = _userManager.Users.ToList();
            var userDtos = new List<AccountManagementDTO>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new AccountManagementDTO
                {
                    Username = user.UserName ?? "Unknown User",
                    Email = user.Email ?? "Email not provided",
                    PhoneNumber = user.PhoneNumber ?? "Phone not provided",
                    Role = roles.FirstOrDefault() ?? "No Role"
                });
            }
            return View(userDtos);
        }

    }
}
