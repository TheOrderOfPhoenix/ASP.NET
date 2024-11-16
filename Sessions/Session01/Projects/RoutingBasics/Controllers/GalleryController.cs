using Microsoft.AspNetCore.Mvc;

namespace C02_RoutingBasics.Controllers
{
    public class GalleryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Main()
        {
            return View();
        }
    }
}
