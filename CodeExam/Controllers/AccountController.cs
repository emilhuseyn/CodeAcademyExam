using CodeExam.Helpers;
using CodeExam.Models;
using CodeExam.ModelViews.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeExam.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","Please enter");
                return View(registerVm);
            }
            AppUser appUser = new AppUser()
            {
                Name = registerVm.Name,
                Surname = registerVm.Surname,
                Email = registerVm.Email,
                UserName = registerVm.Username
            };
            var result = await _userManager.CreateAsync(appUser,registerVm.Password);
            if(!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }
            await _userManager.AddToRoleAsync(appUser,RoleEnum.Admin.ToString());
            return RedirectToAction("Login");
            
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please enter");
                return View(loginVm);
            }
            var user =await _userManager.FindByNameAsync(loginVm.Username);
            if(user == null)
            {
                ModelState.AddModelError("Username", "Username could not found");
                return View();
            }
            else
            {
                var result= await _userManager.CheckPasswordAsync(user, loginVm.Password);
                if (!result)
                {
                    ModelState.AddModelError("Password", "Password is wrong");
                    return View(loginVm);
                }
                await _signInManager.SignInAsync(user, false);

            }
            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task CreateRole()
        {
            foreach(var item in Enum.GetValues(typeof(RoleEnum)))
            {
                if(!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = item.ToString()
                    });
                }
            }
        }
    }
}
