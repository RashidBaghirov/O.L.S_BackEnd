using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;
using OptimunLegalServices.Utilities.Enum;
using OptimunLegalServices.ViewModel;

namespace OptimunLegalServices.Areas.OptimunAdmin.Controllers
{
    [Authorize(Roles = "superadmin")]
    [Area("OptimunAdmin")]
    public class AdminRegisterController : Controller
    {
        private readonly UserManager<User> _usermanager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly OptimunDbContext _context;

        public AdminRegisterController(UserManager<User> usermanager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, OptimunDbContext context)
        {
            _usermanager = usermanager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM account)
        {
            TempData["Register"] = false;
            if (!ModelState.IsValid) return View();
            User user = new()
            {
                FullName = string.Concat(account.Firstname, " ", account.Lastname),
                Email = account.Email,
                UserName = account.Username,
                EmailConfirmed = true

            };
            IdentityResult result = await _usermanager.CreateAsync(user, account.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError message in result.Errors)
                {
                    ModelState.AddModelError("", message.Description);
                }
                return View();
            }
            TempData["Register"] = true;
            await _usermanager.AddToRoleAsync(user, AdminRoles.superadmin.ToString());
            return RedirectToAction("login", "adminLogin");

        }

        public async Task CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole(AdminRoles.superadmin.ToString()));
            await _roleManager.CreateAsync(new IdentityRole(AdminRoles.admin.ToString()));
        }


    }
}

