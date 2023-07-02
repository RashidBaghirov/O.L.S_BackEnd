using Microsoft.AspNetCore.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OptimunLegalServices.Entities
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }

    }
}
