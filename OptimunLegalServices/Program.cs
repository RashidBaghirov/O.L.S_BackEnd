using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OptimunLegalServices.DAL;
using OptimunLegalServices.Entities;
using OptimunLegalServices.Services;

namespace OptimunLegalServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<LayoutService>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<OptimunDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });



            builder.Services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = false;
                opt.Password.RequiredUniqueChars = 1;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 5;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnm_-1234567890.QWERTYUIOPASDFGHJKLZXCVBNM:)( ";
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<OptimunDbContext>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=login}/{search?}"
                );
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{search?}");
            });

            app.Run();
        }
    }
}