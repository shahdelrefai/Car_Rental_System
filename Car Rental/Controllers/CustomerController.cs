using Microsoft.AspNetCore.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DataAccess _dataAccess;

        public CustomerController()
        {
            _dataAccess = new DataAccess();
            if (!_dataAccess.Connect())
            {
                throw new Exception("Database connection failed.");
            }
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult TryLogin(string email, string password)
        {
            Console.WriteLine(email);
            Console.WriteLine(password);
            bool isSuccess = _dataAccess.Login(email, password); // Check login credentials
            if (isSuccess)
            {
                // If login is successful, redirect to the car reservation page
                return RedirectToAction("Reserve");
            }
            else
            {
                // Show error message if login fails
                ViewData["Message"] = "Invalid email or password.";
                return View("Login");
            }
        }

        public IActionResult Register()
        {            
            return View();
        }

        // Customer registration
        [HttpPost]
        public IActionResult TryRegister(string name, string email, string password, string phoneNum)
        {
            var customer = new Customer
            {
                Name = name,
                Email = email,
                Password = password,
                PhoneNum = phoneNum
            };
            var message = _dataAccess.RegisterCustomer(customer); // Register new customer
            ViewData["Message"] = message; // Pass success or error message to the view
            return RedirectToAction("Register");
        }

        public IActionResult Reserve()
        {
            List<Car> cars = _dataAccess.GetCarsFiltered(status: 0);

            return View("Reserve", cars);
        }

        // Reserve a car
        [HttpPost]
        public IActionResult TryReserveCar(int customerId, string plateId, int payment, DateOnly startDate, DateOnly endDate)
        {
            Random Random = new Random();
            int cost = Random.Next(50, 200); // Generate a random cost for the reservation
            var message = _dataAccess.ReserveCar(customerId, plateId, payment, cost, startDate, endDate); // Reserve the car
            ViewData["Message"] = message; // Show success or error message
            List<Car> cars = _dataAccess.GetCarsFiltered(status: 0);
            return View("Reserve", cars);
        }
    }
}
