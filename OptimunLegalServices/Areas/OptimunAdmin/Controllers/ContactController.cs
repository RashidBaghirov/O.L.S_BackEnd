using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;
using Microsoft.EntityFrameworkCore;

namespace OptimunLegalServices.Areas.OptimunAdmin.Controllers
{
    [Area("OptimunAdmin")]
    public class ContactController : Controller
    {
        private readonly OptimunDbContext _context;

        public ContactController(OptimunDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ContactUs> messages = _context.ContactUs.Include(x=>x.PracticeArea).ToList();
            return View(messages);
        }

        public IActionResult Replace(int id)
        {
            if (id == 0) return NotFound();
            ContactUs contact = _context.ContactUs.Include(x => x.PracticeArea).FirstOrDefault(x => x.Id == id);
            if (contact is null) return NotFound();
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Replace(int id, string replace)
        {
            TempData["Contact"] = false;
            if (id == 0) return NotFound();
            ContactUs contact = _context.ContactUs.Include(x => x.PracticeArea).FirstOrDefault(x => x.Id == id);
            if (contact is null) return NotFound();
            MailMessage message = new MailMessage();
            message.From = new MailAddress("optimunlegalservices@gmail.com", "Optimun Legal Services");
            message.To.Add(new MailAddress(contact.Email));
            message.Subject = "Optimun Legal Services Support";
            message.Body = string.Empty;
            message.Body = $"{replace}";

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.Credentials = new NetworkCredential("optimunlegalservices@gmail.com", "ppmzmlxewdyeaqez");
            smtpClient.Send(message);
            contact.IsReply = true;
            _context.SaveChanges();
            TempData["Contact"] = true;
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            TempData["Delete"] = false;
            if (id == 0)  return NotFound();
            ContactUs contact = _context.ContactUs.Include(x => x.PracticeArea).FirstOrDefault(x => x.Id == id);
            if (contact is null) return NotFound();
            _context.ContactUs.Remove(contact);
            _context.SaveChanges();
            TempData["Delete"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
