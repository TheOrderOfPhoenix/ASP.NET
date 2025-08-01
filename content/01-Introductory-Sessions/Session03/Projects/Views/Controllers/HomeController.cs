﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Views.Models;

namespace Views.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}