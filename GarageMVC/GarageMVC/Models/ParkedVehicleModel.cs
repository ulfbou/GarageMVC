using System.ComponentModel.DataAnnotations;

namespace GarageMVC.Models
{
    public class ParkedVehicleModel
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int NumberOfWheels { get; set; } = default!;
    }
}
