namespace GarageMVC.ViewModels
{
    public class OverviewPageViewModel
    {
        public uint PlacesRemaining { get;}
        public IEnumerable<VehicleOverviewViewModel> VehicleOverviews { get;}
    }
}
