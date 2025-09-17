using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Bookstore_MVC.Models;
using Microsoft.Identity.Client;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Bookstore_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        private bool Locked = false;
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }
        // roles :
        // admin
        // staff
        // customer
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserDto user)
        {
            var username = User.Identity.Name;
            if (username != null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // Return 400 Bad Request with the validation messages.
                return BadRequest(ModelState);
            }


            var newUser = new IdentityUser { UserName = user.username };
            var result = await _userManager.CreateAsync(newUser, user.password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                // Again return 400 so the client knows something went wrong.
                return BadRequest(ModelState);
            }

            await _userManager.AddToRoleAsync(newUser, "Customer");
            _logger.LogInformation($"Created user with {user.username} name");

            await _signInManager.SignInAsync(newUser, isPersistent: true);
            // 200 OK with a redirect or 201 Created – choose what fits your API style.
            return RedirectToAction("Index", "Book");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserDto user)
        {
            var username = User.Identity.Name;
            if (username != null)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var login = await _signInManager.PasswordSignInAsync(user.username, user.password, user.rememberme, Locked);
                if (login.Succeeded)
                {
                    _logger.LogInformation($"User {user.username} logged in.");
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                    return BadRequest(ModelState); ;
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return BadRequest(ModelState);
            }
        }
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
    }
}
