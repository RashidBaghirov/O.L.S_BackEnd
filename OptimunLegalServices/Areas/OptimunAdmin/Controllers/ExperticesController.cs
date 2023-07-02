using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;
using OptimunLegalServices.Utilities.Extension;
using System.Data;

namespace OptimunLegalServices.Areas.OptimunAdmin.Controllers
{
    [Authorize(Roles = "superadmin,admin")]

    [Area("OptimunAdmin")]

    public class ExperticesController : Controller
    {
        private readonly OptimunDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ExperticesController(OptimunDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((double)_context.Expertices.Count() / 5);
            ViewBag.CurrentPage = page;

            IEnumerable<Expertice> expertices = _context.Expertices.AsNoTracking().Skip((page - 1) * 5).Take(5).AsEnumerable();

            return View(expertices);
        }


        [HttpPost]
        public IActionResult Index(string search, int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((double)_context.Expertices.Count() / 5);
            ViewBag.CurrentPage = page;

            IEnumerable<Expertice> expertices = _context.Expertices.AsNoTracking().Skip((page - 1) * 5).Take(5).AsEnumerable();

            if (!string.IsNullOrEmpty(search))
            {
                expertices = expertices.Where(x => x.Titles_expertice.ToLower().StartsWith(search.ToLower().Substring(0, Math.Min(search.Length, 3)))).ToList();
            }

            return View(expertices);
        }

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Expertice newExp)
        {
            TempData["Create"] = false;
            if (!ModelState.IsValid)
            {
                foreach (string message in ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }
            }
            if (!newExp.Img.IsValidFile("image/"))
            {
                ModelState.AddModelError(string.Empty, "Please choose image file");
                return View();
            }
            Expertice expertice = new()
            {
                Titles_expertice = newExp.Titles_expertice,
                Infos = newExp.Infos,
            };

            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            expertice.Image = await newExp.Img.CreateImage(imagefolderPath, "Expertice");
            _context.Expertices.Add(expertice);
            _context.SaveChanges();
            TempData["Create"] = true;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            Expertice expertice = _context.Expertices.FirstOrDefault(x => x.Id == id);
            if (expertice is null) return NotFound();
            return View(expertice);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Expertice edited)
        {
            TempData["Edit"] = false;

            Expertice expertice = _context.Expertices.FirstOrDefault(x => x.Id == id);
            if (expertice is null) return NotFound();
            if (edited.Img is not null)
            {
                if (!edited.Img.IsValidFile("image/"))
                {
                    ModelState.AddModelError(string.Empty, "Please choose image file");
                    return View();
                }
                if (!edited.Img.IsValidLength(2))
                {
                    ModelState.AddModelError(string.Empty, "Please choose image which size is maximum 2MB");
                    return View();
                }
                await AdjustPlantPhoto(edited.Img, expertice);
            }


            expertice.Titles_expertice = edited.Titles_expertice;
            expertice.Infos = edited.Infos;
            _context.SaveChanges();
            TempData["Edit"] = true;
            return RedirectToAction(nameof(Index));
        }

        private async Task AdjustPlantPhoto(IFormFile image, Expertice expertice)
        {
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            string filepath = Path.Combine(imagefolderPath, "Expertice", expertice.Image);
            ExtensionMethods.DeleteImage(filepath);
            expertice.Image = await image.CreateImage(imagefolderPath, "Expertice");
        }




        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();
            Expertice expertice = _context.Expertices.FirstOrDefault(x => x.Id == id);
            if (expertice is null) return NotFound();
            return View(expertice);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            Expertice expertice = _context.Expertices.FirstOrDefault(x => x.Id == id);
            if (expertice is null) return NotFound();
            return View(expertice);
        }

        [HttpPost]
        public IActionResult Delete(int id, Expertice delete)
        {
            TempData["Delete"] = false;
            if (id != delete.Id) return NotFound();
            Expertice? expertice = _context.Expertices.FirstOrDefault(s => s.Id == id);
            if (expertice is null) return NotFound();

            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            string filepath = Path.Combine(imagefolderPath, "Expertice", expertice.Image);
            ExtensionMethods.DeleteImage(filepath);

            _context.Expertices.Remove(expertice);
            _context.SaveChanges();

            TempData["Delete"] = true;

            return RedirectToAction(nameof(Index));
        }
    }
}
