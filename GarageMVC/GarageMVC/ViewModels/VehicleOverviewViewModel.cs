using GarageMVC.Models;

namespace GarageMVC.ViewModels
{
    public class VehicleOverviewViewModel
    {
        public int Id {get; }
        public string? Type { get; }
        public string? RegistrationNumber { get; }
        public DateTime TimeStamp { get; }
        public VehicleOverviewViewModel(ParkedVehicleModel parkedVehicle)
        {
            Type = parkedVehicle.Type;
            RegistrationNumber = parkedVehicle.RegistrationNumber;
            TimeStamp = parkedVehicle.TimeStamp;
        }
    }

}
