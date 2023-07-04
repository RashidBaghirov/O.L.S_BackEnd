using Microsoft.AspNetCore.Mvc;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;

namespace OptimunLegalServices.Controllers
{
    public class ExperticeController : Controller
    {
        private readonly OptimunDbContext _context;

        public ExperticeController(OptimunDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Expertice> expertices = _context.Expertices.ToList();
            ViewBag.Practice = _context.PracticeAreas.ToList();
            return View(expertices);
        }
    }
}
