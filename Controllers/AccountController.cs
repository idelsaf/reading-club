using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReadingClubWebApp.Data;
using ReadingClubWebApp.Models;
using ReadingClubWebApp.ViewModels;

namespace ReadingClubWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
            
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Event");
                    }
                }

                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginVM);
        }
    }
}
