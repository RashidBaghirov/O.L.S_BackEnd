using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;
using System.Data;

namespace OptimunLegalServices.Areas.OptimunAdmin.Controllers
{
    [Authorize(Roles = "superadmin,admin")]

    [Area("OptimunAdmin")]

    public class SubscribeController : Controller
    {
        private readonly OptimunDbContext _context;

        public SubscribeController(OptimunDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((double)_context.Subscribes.Count() / 8);
            ViewBag.CurrentPage = page;
            List<Subscribe> subscribes = _context.Subscribes.Skip((page - 1) * 8).Take(8).ToList();
            return View(subscribes);
        }

        [HttpPost]
        public IActionResult Index(string search, int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((double)_context.Subscribes.Count() / 8);
            ViewBag.CurrentPage = page;
            List<Subscribe> subscribes = _context.Subscribes.Skip((page - 1) * 8).Take(8).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                subscribes = subscribes.Where(x => x.Email.ToLower().StartsWith(search.ToLower().Substring(0, Math.Min(search.Length, 2)))).ToList();
            }
            return View(subscribes);
        }

        public IActionResult Delete(int id)
        {
            TempData["Delete"] = false;

            if (id == 0) return NotFound();


            Subscribe? subscribe = _context.Subscribes.FirstOrDefault(x => x.Id == id);

            if (subscribe is null) return NotFound();

            _context.Subscribes.Remove(subscribe);
            _context.SaveChanges();
            TempData["Delete"] = true;
            return RedirectToAction(nameof(Index));
        }

    }
}
