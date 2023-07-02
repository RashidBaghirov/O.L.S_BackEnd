using Microsoft.AspNetCore.Mvc;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;

namespace OptimunLegalServices.Controllers
{
    public class ContactController : Controller
    {
        private readonly OptimunDbContext _context;

        public ContactController(OptimunDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Area = _context.PracticeAreas.ToList();
            return View();
        }



        [HttpPost]
        public IActionResult Index(string name, string surname, string email, string phoneNumber, string comment, int practiceId)
        {
            TempData["Contact"] = false;

            ContactUs contactUs = new()
            {
                FirstName = name,
                LastName = surname,
                Email = email,
                PhoneNumber = phoneNumber,
                Comment = comment,
                PracticeAreaId = practiceId,
                SendTime = DateTime.Now,

                IsReply = false
            };

            _context.ContactUs.Add(contactUs);
            _context.SaveChanges();
            TempData["Contact"] = true;
            return RedirectToAction("Index", "Home");
        }
    }
}
