using LawProject.Models;
using LawProject.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace LawProject.Controllers
{
    public class AccountController : Controller
    {
        public readonly UserManager<AppUser> _userManager;
        public readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager=userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM newUser)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = new AppUser
            {
                Name = newUser.Name,
                Surname = newUser.Surname,
                Email=newUser.Email,
                UserName=newUser.Username,

            };

            IdentityResult result = await _userManager.CreateAsync(user, newUser.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
            
        }


        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM user)
        {
            if (!ModelState.IsValid) return View();
            AppUser existed = await _userManager.FindByEmailAsync(user.UsernameOrEmail);

            if (existed==null)
            {
                existed = await _userManager.FindByNameAsync(user.UsernameOrEmail);

                if(existed == null){
                    ModelState.AddModelError(string.Empty, "Username or Password is not correct");
                    return View();
                }
            }

            var result = await _signInManager.PasswordSignInAsync(existed, user.Password, user.IsRemember, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "Block olunub gozleyin");
                return View();
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username or Password is not correct");
                return View();
            }
            

            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }
}
