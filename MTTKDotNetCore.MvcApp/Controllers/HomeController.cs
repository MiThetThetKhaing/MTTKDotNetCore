using Microsoft.AspNetCore.Mvc;
using MTTKDotNetCore.MvcApp.Models;
using System.Diagnostics;

namespace MTTKDotNetCore.MvcApp.Controllers
{
    // https://localhost:3000/Home/Index
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Message = "Hello from Viewbag";

            HomeResponseModel model = new HomeResponseModel();
            model.message = "Hello from model";

            return View(model);
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

        public IActionResult Index2()
        {
            return View();
        }
    }
}
