using Microsoft.AspNetCore.Mvc;

namespace OptimunLegalServices.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Detail()
        {
            return View();
        }
    }
}
