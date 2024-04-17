using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarageMVC.Models
{
	public class Garage
	{
		[Key]
		public int Id { get; set; }
		public int Spots { get; set; }
		public int OccupiedSpots { get; set; }
		public ParkedVehicleModel? Car { get; set; }
		[ForeignKey("CarId")]
		public int? CarId { get; set; }
	}
}
