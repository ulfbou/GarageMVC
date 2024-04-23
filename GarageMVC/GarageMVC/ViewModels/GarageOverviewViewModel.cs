
namespace GarageMVC.ViewModels
{
    public class GarageOverviewViewModel
    {
        public int SumOfAllWheels { get; internal set; }
        public List<string> VehicleType { get; internal set; }
        public double SumOfPrice { get; internal set; }
        public List<string> VehicleTypes { get; internal set; }
        public List<string> VehicleColors { get; internal set; }
        public List<string> VehicleBrands { get; internal set; }
    }
}
