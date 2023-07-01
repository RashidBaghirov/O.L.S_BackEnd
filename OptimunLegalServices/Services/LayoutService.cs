using Microsoft.EntityFrameworkCore;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;

namespace OptimunLegalServices.Services
{
    public class LayoutService
    {
        private readonly OptimunDbContext _context;

        public LayoutService(OptimunDbContext context)
        {
            _context = context;
        }
        public List<ContactUs> GetContactUs()
        {
            List<ContactUs> contacts = _context.ContactUs.ToList();
            return contacts;
        }
    }
}
