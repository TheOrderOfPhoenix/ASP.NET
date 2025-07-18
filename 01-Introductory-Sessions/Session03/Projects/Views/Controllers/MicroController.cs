using Microsoft.AspNetCore.Mvc;

namespace Views.Controllers
{
    public class MicroController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
