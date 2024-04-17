using GarageMVC.Models;
using System.ComponentModel;

namespace GarageMVC.ViewModels
{
    public class VehicleOverviewViewModel
    {
        public int Id {get; }
        public string? Type { get; }
        [DisplayName("Registration Number")]
        public string? RegistrationNumber { get; }
        [DisplayName("Time of Parking")]
        public DateTime TimeStamp { get; }
        public VehicleOverviewViewModel(ParkedVehicleModel parkedVehicle)
        {
            Type = parkedVehicle.Type;
            RegistrationNumber = parkedVehicle.RegistrationNumber;
            TimeStamp = parkedVehicle.TimeStamp;
        }
    }

}
