using System.ComponentModel.DataAnnotations;
namespace GarageMVC.Models
{
    public class ParkedVehicleModel
    {
        [Key]
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Color { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int NumberOfWheels { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
