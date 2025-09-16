using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Bookstore_MVC.Models;
using Microsoft.Identity.Client;

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
        [HttpPost]
        public async Task<IActionResult> Register(UserDto user)
        {
            if (ModelState.IsValid)
            {
                // create user object
                var newUser = new IdentityUser { UserName = user.username };
                // adds new user with password
                var result = await _userManager.CreateAsync(newUser, user.password);
                // adds assign newly created user to customer
                await _userManager.AddToRoleAsync(newUser, "Customer");

                if (result.Succeeded)
                {
                    // if success, go to index
                    await _signInManager.SignInAsync(newUser, isPersistent: true);
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(user);
            }
            else
            {
                return View(user);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserDto user)
        {

            if (ModelState.IsValid)
            {
                var login = await _signInManager.PasswordSignInAsync(user.username, user.password, user.rememberme, Locked);
                if (login.Succeeded)
                {
                    _logger.LogInformation($"User {user.username} logged in.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt");
                    return View(user);
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt");
                return View(user);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
