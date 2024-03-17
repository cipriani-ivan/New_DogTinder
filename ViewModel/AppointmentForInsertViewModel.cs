namespace NewDogTinder.ViewModels;

public class AppointmentForInsertViewModel
{
    [Required]
    public DateTime Time { get; set; }
    [Range(1, int.MaxValue)]
    [Required]
    public int PlaceId { get; set; }
    [Range(1, int.MaxValue)]
    [Required]
    public int DogId { get; set; }
}
