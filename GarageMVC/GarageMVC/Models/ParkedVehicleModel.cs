using System.ComponentModel.DataAnnotations;
namespace GarageMVC.Models
{
    public class ParkedVehicleModel
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = default!;
        public string Color { get; set; } = default!;
        public string RegistrationNumber { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public string Model { get; set; } = default!;
        public int NumberOfWheels { get; set; } = default!;
    }
}
