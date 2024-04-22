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


		[Required(ErrorMessage = "Brand is required.")]
		public string Brand { get; set; } = string.Empty;

		[Required(ErrorMessage = "Model is required.")]
		public string Model { get; set; } = string.Empty;

		[Range(1, int.MaxValue, ErrorMessage = "Number of wheels must be a positive integer.")]
		public int NumberOfWheels { get; set; } = default!;

		[RegularExpression(@"^[a-zA-Z]{3}[0-9]{3}$", ErrorMessage = "Registeration Number is combination of 3 letters and 3 numbers. ex: ABC123")]
		public string RegistrationNumber { get; set; } = string.Empty;
		public DateTime TimeStamp { get; private set; } = DateTime.Now;

		internal string ParkedDuration = string.Empty;
		public int TotalCost { get; set; }
		internal static int pricePerHour = 15;
		internal string parkedAt = string.Empty;
		[Required(ErrorMessage = "Parking Spot Number is required.")]
		[Range(1, 100, ErrorMessage = "Parking Spot Number must be between 1 and 100.")]
		public int ParkingSpotNumber { get; set; }
		private const int maxSpotNumber = 100;
	}
}
