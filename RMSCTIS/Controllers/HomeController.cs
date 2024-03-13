using Microsoft.AspNetCore.Mvc;
using RMSCTIS.Models;
using System.Diagnostics;

namespace RMSCTIS.Controllers
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

       
    }
}
