using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimunLegalServices.Areas.OptimunAdmin.ViewModel;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OptimunLegalServices.Areas.OptimunAdmin.Controllers
{
    [Authorize(Roles = "superadmin,admin")]

    [Area("OptimunAdmin")]
    public class HomeController : Controller
    {
        private readonly OptimunDbContext _context;

        public HomeController(OptimunDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ContactUs> contacts = _context.ContactUs.Include(x => x.PracticeArea).ToList();
            List<PracticeArea> practiceAreas = _context.PracticeAreas.Include(x => x.ContactUs).ToList();
            List<PRacticeAreaVM> PRacticeAreaVM = practiceAreas.Select(ba => new PRacticeAreaVM
            {
                Name = ba.Name,
                ContactCount = contacts.Count(x => x.PracticeAreaId == ba.Id)
            }).ToList();

            var labels = practiceAreas.Select(ba => ba.Name).ToList();
            var datasetData = PRacticeAreaVM.Select(cv => cv.ContactCount).ToList();
            ViewBag.DatasetData = datasetData;
            ViewBag.Labels = labels;
            return View();
        }

    }
}
