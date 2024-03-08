namespace NewDogTinder.ViewModels;

public class AppointmentViewModel
{
	public int AppointmentId { get; set; }
	public DateTime Time { get; set; }
	public PlaceViewModel Place { get; set; }
	public DogViewModel Dog { get; set; }
}
