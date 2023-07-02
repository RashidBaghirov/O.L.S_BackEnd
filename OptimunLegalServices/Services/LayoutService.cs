using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;

namespace OptimunLegalServices.Services
{
    public class LayoutService
    {
        private readonly OptimunDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<User> _userManager;

        public LayoutService(OptimunDbContext context, IHttpContextAccessor accessor, UserManager<User> userManager)
        {
            _context = context;
            _accessor = accessor;
            _userManager = userManager;
        }
        public List<ContactUs> GetContactUs()
        {
            List<ContactUs> contacts = _context.ContactUs.ToList();
            return contacts;
        }
        public async Task<User> GetUserFullName()
        {
            var user = await _userManager.GetUserAsync(_accessor.HttpContext.User);
            return user;
        }
    }
}
