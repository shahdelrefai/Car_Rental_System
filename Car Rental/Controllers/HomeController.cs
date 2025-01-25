using System.Diagnostics;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataAccess _dataAccess = new DataAccess();    

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Office-related actions
        public IActionResult Office()
        {
            return View();
        }

        // Customer-related actions
        public IActionResult Customer()
        {
            return View();
        }
    }
}
