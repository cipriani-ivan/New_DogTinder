namespace NewDogTinder.EFDataAccessLibrary.Models;

public class Appointment
{
	public int AppointmentId { get; set; }
	[Required]
	public DateTime Time { get; set; }

	[Required]
	public Place Place { get; set; }
	
	[Required]
	public Dog Dog { get; set; }
}
