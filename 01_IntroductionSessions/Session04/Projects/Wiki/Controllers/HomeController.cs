using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Wiki.Models;

namespace Wiki.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<string> list = new List<string>();
            list.Add("harry_potter");
            list.Add("hermione_granger");
            ViewBag.ListOfNames = list;

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        
        public IActionResult Person(string name)
        {
            Character character = new Character();
            
            if (name == "hermione_granger")
            {
                character.Name = "Hermione Granger";
                character.Birthdate = "19 September 1979";
                character.Quote = "“Stop, stop, stop! You’re going to take someone’s eye out. Besides, you’re saying it wrong. It’s leviosa, not leviosar!”";
                character.ImageName = "hermione_granger.png";
            }
            else if (name == "harry_potter")
            {
                character.Name = "Harry Potter";
                character.Birthdate = "31 July 1980";
                character.Quote = "\"I’ll be in my bedroom, making no noise and pretending I’m not there.\"";
                character.ImageName = "harry_potter.png";
            }
            else
            {
                return NotFound();
            }
            
            return View(character);
        }


        public IActionResult Person2(string id)
        {
            Character character = new Character();
            if (id == "harry_potter")
            {
                character.Name = "Harry Potter";
                character.Birthdate = "31 July 1980";
                character.Quote = "\"I’ll be in my bedroom, making no noise and pretending I’m not there.\"";
                character.ImageName = "harry_potter.png";
            }
            else if (id == "hermione_granger")
            {
                character.Name = "Hermione Granger";
                character.Birthdate = "19 September 1979";
                character.Quote = "“Stop, stop, stop! You’re going to take someone’s eye out. Besides, you’re saying it wrong. It’s leviosa, not leviosar!”";
                character.ImageName = "hermione_granger.png";
            }
            return View(character);
        }

        public string Test()
        {
            return "Test2";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}