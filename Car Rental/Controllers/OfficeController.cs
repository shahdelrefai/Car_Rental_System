using Microsoft.AspNetCore.Mvc;
using CarRental.Models;

namespace CarRental.Controllers
{
    public class OfficeController : Controller
    {
        private readonly DataAccess _dataAccess;

        public OfficeController()
        {
            _dataAccess = new DataAccess();
            if (!_dataAccess.Connect())
            {
                throw new Exception("Database connection failed.");
            }
        }
        // Action to Add a Car
        public IActionResult AddCar()
        {
            // You can return a view with a form to add a car
            return View();
        }

        [HttpPost]
        public IActionResult TryAddCar(string plateId, string model, int year, int status, int officeId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Car car = new Car
                    {
                        PlateId = plateId,
                        Model = model,
                        Year = year,
                        Status = status,
                        OfficeId = officeId
                    };
                    // Call the DataAccess method to add the car
                    string mess = _dataAccess.AddCar(car);

                    // Set success message in ViewData to display it on the AddCar view
                    ViewData["Message"] = mess;
                }
                catch (Exception ex)
                {
                    ViewData["Message"] = $"Error adding car: {ex.Message}";
                }
            }

            return View("AddCar");
        }

        // Action to Update a Car
        public IActionResult UpdateCar()
        {
            // You can return a view with a form to update a car
            return View();
        }

        // Action to Search Cars
        public IActionResult SearchCars()
        {
            List<Car> cars = _dataAccess.GetCarsFiltered();

            return View("SearchCars", cars);
        }

        public IActionResult TrySearchCars(string? model, int? year, int? status, int? customer)
        {
            List<Car> cars;
            if (customer != null && model != null)
            {
                cars = _dataAccess.GetCarsFilteredByResvCustomerAndModel(model, (int)customer);
            }
            else if (customer != null )
            {
                cars = _dataAccess.GetCarsFilteredByResvCustomer((int)customer);
            }
            else
            {
                cars = _dataAccess.GetCarsFiltered(model, year, status);
            }
            
            ViewData["Message"] = cars.Count > 0 ? "Cars found." : "No cars found.";

            return View("SearchCars", cars);
        }
    }

}
