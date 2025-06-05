using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCdemo.Models;

namespace MVCdemo.Controllers
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
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TestPage()
        {
            ConsumerManager consumer = new ConsumerManager();

            // use ViewBag to pass data to view
            ViewBag.Consumer = consumer.getConsumer();
            ViewBag.Header = "This is using ViewBag";

            //return view(consumer.getConsumer(); is to pass data from controller to view using strongly type view
            //its better to use strongly type because its easier to debug and provide runtime error
            //if want to give view more than 1 data from multiple consumer, combine those data first and then pass it to view.
            return View(consumer.getConsumer());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
