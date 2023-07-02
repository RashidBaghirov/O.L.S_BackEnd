using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;

namespace OptimunLegalServices.Controllers
{
    public class HomeController : Controller
    {
        private readonly OptimunDbContext _context;
        public HomeController(OptimunDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Subscribe(string email)
        {
            TempData["Subscribe"] = false;
            if (email is null) return Redirect(Request.Headers["Referer"].ToString());
            bool Isdublicate = _context.Subscribes.Any(c => c.Email == email);

            if (Isdublicate)
            {
                return Redirect(Request.Headers["Referer"].ToString());
            }
            Subscribe subscribe = new()
            {
                Email = email
            };
            _context.Subscribes.Add(subscribe);
            _context.SaveChanges();
            TempData["Subscribe"] = true;
            return Redirect(Request.Headers["Referer"].ToString());
        }


    }
}
