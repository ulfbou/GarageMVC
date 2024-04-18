using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace GarageMVC.Models
{
    public class ParkedVehicleModel
    {
        public string? ParkedDuration ;

        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;


        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int NumberOfWheels { get; set; } = default!;


        [RegularExpression(@"^[A-Z]{3}[0-9]{3}$", ErrorMessage = "Registeration Number is combination of 3 letters and 3 numbers. ex: ABC123")]
        public string RegistrationNumber { get; set; } = string.Empty;

        //[Range(1, 100)]
        //public int ParkingSpot {  get; set; } = default!;

        [ReadOnly(true)]
        public DateTime TimeStamp { get; private set; } = DateTime.Now;
        //public DateTime TimeStamp { get; set; } = default!;
    }
}
