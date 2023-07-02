using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;
using System.Data;

namespace OptimunLegalServices.Areas.OptimunAdmin.Controllers
{
    [Authorize(Roles = "superadmin,admin")]
    [Area("OptimunAdmin")]
    public class PracticeAreaController : Controller
    {
        private readonly OptimunDbContext _context;

        public PracticeAreaController(OptimunDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((double)_context.PracticeAreas.Count() / 8);
            ViewBag.CurrentPage = page;
            List<PracticeArea> cities = _context.PracticeAreas.OrderBy(x => x.Name).AsNoTracking().Skip((page - 1) * 8).Take(8).ToList();
            return View(cities);
        }

        [HttpPost]
        public IActionResult Index(string search, int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((double)_context.PracticeAreas.Count() / 8);
            ViewBag.CurrentPage = page;
            List<PracticeArea> cities = _context.PracticeAreas.OrderBy(x => x.Name).AsNoTracking().Skip((page - 1) * 8).Take(8).ToList();
            if (!string.IsNullOrEmpty(search))
            {
                cities = cities.Where(x => x.Name.ToLower().StartsWith(search.ToLower().Substring(0, Math.Min(search.Length, 3)))).ToList();
            }
            return View(cities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PracticeArea newarea)
        {
            TempData["Create"] = false;
            if (!ModelState.IsValid)
            {
                foreach (string message in ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }
                return View();
            }
            bool Isdublicate = _context.PracticeAreas.Any(c => c.Name == newarea.Name);

            if (Isdublicate)
            {
                ModelState.AddModelError("", "You cannot enter the same data again");
                return View();
            }
            _context.PracticeAreas.Add(newarea);
            _context.SaveChanges();
            TempData["Create"] = true;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            PracticeArea? area = _context.PracticeAreas.FirstOrDefault(c => c.Id == id);
            if (area is null) return NotFound();
            return View(area);
        }

        [HttpPost]
        public IActionResult Edit(int id, PracticeArea edited)
        {
            TempData["Edit"] = false;

            if (id != edited.Id) return NotFound();
            PracticeArea? area = _context.PracticeAreas.FirstOrDefault(c => c.Id == id);
            if (area is null) return NotFound();
            bool duplicate = _context.PracticeAreas.Any(c => c.Name == edited.Name && area.Name != edited.Name);
            if (duplicate)
            {
                ModelState.AddModelError("Name", "This  Practice name is now available");
                return View();
            }
            area.Name = edited.Name;
            _context.SaveChanges();
            TempData["Edit"] = true;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();
            PracticeArea? area = _context.PracticeAreas.FirstOrDefault(c => c.Id == id);
            return area is null ? NotFound() : View(area);
        }



        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            PracticeArea? area = _context.PracticeAreas.FirstOrDefault(c => c.Id == id);
            if (area is null) NotFound();
            return View(area);
        }

        [HttpPost]
        public IActionResult Delete(int id, PracticeArea delete)
        {
            TempData["Delete"] = false;

            if (id != delete.Id) return NotFound();
            PracticeArea? area = _context.PracticeAreas.FirstOrDefault(c => c.Id == id);
            if (area is null) return NotFound();
            _context.PracticeAreas.Remove(area);
            _context.SaveChanges();
            TempData["Delete"] = true;
            return RedirectToAction(nameof(Index));
        }
    }

}
