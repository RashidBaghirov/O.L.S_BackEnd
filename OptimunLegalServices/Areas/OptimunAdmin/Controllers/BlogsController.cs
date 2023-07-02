using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;
using OptimunLegalServices.Utilities.Extension;
using System.Reflection.Metadata;

namespace Papa_Jhons.Areas.AdminAreas.Controllers
{
    [Authorize(Roles = "superadmin,admin")]

    [Area("OptimunAdmin")]

    public class BlogsController : Controller
    {
        private readonly OptimunDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogsController(OptimunDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((double)_context.Blogs.Count() / 5);
            ViewBag.CurrentPage = page;

            IEnumerable<Blog> blogs = _context.Blogs.AsNoTracking().Skip((page - 1) * 5).Take(5).AsEnumerable();

            return View(blogs);
        }


        [HttpPost]
        public IActionResult Index(string search, int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((double)_context.Blogs.Count() / 5);
            ViewBag.CurrentPage = page;

            IEnumerable<Blog> blogs = _context.Blogs.AsNoTracking().Skip((page - 1) * 5).Take(5).AsEnumerable();

            if (!string.IsNullOrEmpty(search))
            {
                blogs = blogs.Where(x => x.Title.ToLower().StartsWith(search.ToLower().Substring(0, Math.Min(search.Length, 3)))).ToList();
            }

            return View(blogs);
        }

        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog newBlog)
        {
            TempData["Create"] = false;

            if (!ModelState.IsValid)
            {
                foreach (string message in ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage))
                {
                    ModelState.AddModelError("", message);
                }
            }
            if (!newBlog.Img.IsValidFile("image/"))
            {
                ModelState.AddModelError(string.Empty, "Please choose image file");
                return View();
            }
            Blog blog = new()
            {
                Title = newBlog.Title,
                Desc1 = newBlog.Desc1,
                Date = DateTime.Now
            };
            if (newBlog.Desc2 is not null)
            {
                blog.Desc2 = newBlog.Desc2;
            }
            if (newBlog.Desc3 is not null)
            {
                blog.Desc3 = newBlog.Desc3;
            }
            if (newBlog.Desc4 is not null)
            {
                blog.Desc4 = newBlog.Desc4;
            }
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            blog.Image = await newBlog.Img.CreateImage(imagefolderPath, "Blogs");



            _context.Blogs.Add(blog);
            _context.SaveChanges();
            TempData["Create"] = true;

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();
            Blog blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            if (blog is null) return NotFound();
            return View(blog);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Blog edited)
        {
            TempData["Edit"] = false;
            Blog blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            if (blog is null) return NotFound();

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
                await AdjustPlantPhoto(edited.Img, blog);
            }


            blog.Title = edited.Title;
            blog.Desc1 = edited.Desc1;
            blog.Desc2 = edited.Desc2;
            blog.Desc3 = edited.Desc3;
            blog.Desc4 = edited.Desc4;
            _context.SaveChanges();
            TempData["Edit"] = true;

            return RedirectToAction(nameof(Index));
        }

        private async Task AdjustPlantPhoto(IFormFile image, Blog blog)
        {
            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            string filepath = Path.Combine(imagefolderPath, "Blogs", blog.Image);
            ExtensionMethods.DeleteImage(filepath);
            blog.Image = await image.CreateImage(imagefolderPath, "Blogs");
        }




        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();
            Blog blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            if (blog is null) return NotFound();
            return View(blog);
        }

        public IActionResult Delete(int id)
        {
            if (id == 0) return NotFound();
            Blog blog = _context.Blogs.FirstOrDefault(x => x.Id == id);
            if (blog is null) return NotFound();
            return View(blog);
        }

        [HttpPost]
        public IActionResult Delete(int id, Blog delete)
        {
            TempData["Delete"] = false;

            if (id != delete.Id) return NotFound();
            Blog? blog = _context.Blogs.FirstOrDefault(s => s.Id == id);
            if (blog is null) return NotFound();

            var imagefolderPath = Path.Combine(_env.WebRootPath, "assets", "images");
            string filepath = Path.Combine(imagefolderPath, "Blogs", blog.Image);
            ExtensionMethods.DeleteImage(filepath);

            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            TempData["Delete"] = true;

            return RedirectToAction(nameof(Index));
        }
    }

}
