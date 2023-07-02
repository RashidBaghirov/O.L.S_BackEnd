using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;
using OptimunLegalServices.Utilities.Enum;
using OptimunLegalServices.ViewModel;

namespace OptimunLegalServices.Areas.OptimunAdmin.Controllers
{
    [Area("OptimunAdmin")]
    public class AdminLoginController : Controller
    {

        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly OptimunDbContext _context;

        public AdminLoginController(UserManager<User> usermanager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, OptimunDbContext context)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM account)
        {
            if (!ModelState.IsValid) return View();

            User user = await _usermanager.FindByNameAsync(account.UserName);
            if (user is null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }

            var userRoles = await _usermanager.GetRolesAsync(user);

            if (userRoles.Contains(AdminRoles.superadmin.ToString()) || userRoles.Contains(AdminRoles.admin.ToString()))
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, account.Password, account.RememberMe, true);

                if (!result.Succeeded)
                {
                    if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Due to your efforts, our account was blocked for 5 minutes");
                    }
                    ModelState.AddModelError("", "Username or password is incorrect");
                    return View();
                }
            }
            return RedirectToAction("Index", "Contact");

        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


    }
}
