using Microsoft.AspNetCore.Mvc;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;

namespace OptimunLegalServices.Controllers
{
    public class NewsController : Controller
    {
        private readonly OptimunDbContext _context;

        public NewsController(OptimunDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((double)_context.Blogs.Count() / 8);
            ViewBag.CurrentPage = page;
            List<Blog> blogs = _context.Blogs.Skip((page - 1) * 8).Take(8).ToList();
            return View(blogs);
        }


        public IActionResult Detail(int id)
        {
            if (id == 0) return NotFound();
            Blog blogs = _context.Blogs.FirstOrDefault(x => x.Id == id);
            if (blogs is null) return NotFound();
            return View(blogs);
        }

        public async Task<IActionResult> Search(string search)
        {
            IQueryable<Blog> query = _context.Blogs.Where(x => x.Title.ToLower().Contains(search.ToLower()));


            List<Blog> blog = query.OrderByDescending(x => x.Id)
                .ToList();

            return PartialView("_SearchPartial", blog);
        }
    }
}
