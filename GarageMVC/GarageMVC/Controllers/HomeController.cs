using GarageMVC.Models;
using GarageMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GarageMVC.Controllers
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
            IEnumerable<VehicleOverviewViewModel> vehicleOverviews = new List<VehicleOverviewViewModel>()
            {
                new VehicleOverviewViewModel(new ParkedVehicleModel(){Id = 0, Type = "Car", RegistrationNumber = "ABC123", TimeStamp = new DateTime(2024,1,1) }),
                new VehicleOverviewViewModel(new ParkedVehicleModel(){Id = 1, Type = "Motorcycle", RegistrationNumber = "DEF456", TimeStamp = new DateTime(2024,1,2) }),
                new VehicleOverviewViewModel(new ParkedVehicleModel(){Id = 2, Type = "Truck", RegistrationNumber = "GHI789", TimeStamp = new DateTime(2024,1,3) })
            };
            return View(new OverviewPageViewModel() {PlacesRemaining = 0, VehicleOverviews =vehicleOverviews });
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
