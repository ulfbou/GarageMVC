using System.ComponentModel.DataAnnotations;

namespace GarageMVC.Models
{

    public class ParkedVehicleModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Color is required.")]
        public string Color { get; set; } = string.Empty;

        [Required(ErrorMessage = "Registration number is required.")]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Brand is required.")]
        public string Brand { get; set; } = string.Empty;

        [Required(ErrorMessage = "Model is required.")]
        public string Model { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Number of wheels must be a positive integer.")]
        public int NumberOfWheels { get; set; } = default!;

        [Required(ErrorMessage = "Time of Parking is required")]
        public DateTime TimeStamp { get; set; } = default!;
    }
}
