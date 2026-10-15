using CarRentalApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalApp.Controllers
{
    public class CarController : Controller
    {
        private static List<Car> availableCars = new()
        {
            new Car { Id = 1, Name = "Alto", PricePerDay = 3000 },
            new Car { Id = 2, Name = "Corolla", PricePerDay = 6000 },
            new Car { Id = 3, Name = "Civic", PricePerDay = 8000 },
            new Car { Id = 4,  Name="BMW", PricePerDay=10000},
            new Car { Id = 5, Name = "Mercedes", PricePerDay = 50000 },
            new Car { Id = 6, Name = "Mehran", PricePerDay = 3000 },
            new Car { Id = 7, Name = "Fortuner", PricePerDay = 133000 },
            new Car { Id = 8, Name = "Rolls Royace", PricePerDay = 178000 },
            new Car { Id = 9, Name = "taxi", PricePerDay = 93000 },
            new Car { Id = 10, Name = "abc", PricePerDay = 183000 }

        };

        private static List<Car> bookedCars = new();

        [HttpGet]
        public IActionResult Index()
        {
            return View(availableCars);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Book(int id)
        {
            Car? car = availableCars.FirstOrDefault(c => c.Id == id);

            if (car == null)
                return RedirectToAction("Index");

            availableCars.Remove(car);
            bookedCars.Add(car);

            return RedirectToAction("Booked");
        }

        [HttpGet]
        public IActionResult Booked()
        {
            return View(bookedCars);
        }
    }
}
