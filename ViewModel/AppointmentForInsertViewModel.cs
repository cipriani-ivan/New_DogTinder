namespace NewDogTinder.ViewModels;

public class AppointmentForInsertViewModel
{
    [Required]
    public DateTime Time { get; set; }
    [Required]
    public int PlaceId { get; set; }
    [Required]
    public int DogId { get; set; }
}
