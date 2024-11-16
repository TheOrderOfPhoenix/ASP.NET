using C01_ThePodcastWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace C01_ThePodcastWebsite.Controllers
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
            List<Episode> list = new List<Episode>();
            string description1 = "In this very first episode, we introduce you to the world of technology from a fresh perspective. Whether you're a developer, engineer, or tech enthusiast, this podcast is here to demystify emerging trends in technology. Join us as we break down complex topics into digestible conversations with industry experts. Tune in as we embark on a journey through machine learning, deep learning, artificial intelligence, and beyond. Let's kick things off!";
            string description2 = "n this episode, we dive into the fundamentals of Machine Learning. What exactly is it, and how does it impact our everyday lives? From predictive analytics to recommendation engines, ML is revolutionizing industries across the globe. We'll explore key concepts like supervised learning, unsupervised learning, and real-world applications, including healthcare, finance, and marketing. Listen in to understand how machines are learning to make decisions and predictions with minimal human intervention.";
            string description3 = "Join us as we take a deep dive into Deep Learning, the advanced subset of Machine Learning that powers some of the most exciting AI applications today. Discover how neural networks mimic the human brain, enabling breakthroughs in image recognition, natural language processing, and even autonomous driving. We’ll break down the differences between traditional ML and deep learning, the role of massive datasets, and why this technology is becoming essential in AI research and development.";
            string description4 = "Artificial Intelligence is no longer science fiction—it's shaping the future of how we live and work. In this episode, we explore what AI truly means, its current capabilities, and where it’s headed. From chatbots and virtual assistants to AI-driven decision-making in businesses, we discuss how AI is already being integrated into our daily lives. We'll also touch on the ethical concerns and the importance of responsible AI development. Get ready to envision a future powered by intelligent machines!";
            list.Add(new Episode(0, "Welcome to Tech Insights Podcast!", description1, "2 hr 27 min", "episode-zero.jpg", new DateTime(2024, 10, 1)));
            list.Add(new Episode(1, "Machine Learning 101", description2, "2 hr 3 min", "episode-one.jpg", new DateTime(2024, 10, 8)));
            list.Add(new Episode(2, "Deep Learning Demystified", description3, "1 hr 13 min", "episode-two.jpg", new DateTime(2024, 11, 1)));
            list.Add(new Episode(3, "AI", description4, "1 hr 10 min", "episode-three.png", new DateTime(2024, 11, 8)));
            return View(list);
        }

        public IActionResult Privacy()
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