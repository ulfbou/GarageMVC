
namespace GarageMVC.ViewModels
{
    public class GarageOverviewViewModel
    {
        public int SumOfAllWheels { get; internal set; }
        public List<string> VehicleType { get; internal set; }
        public double SumOfPrice { get; internal set; }
    }
}
