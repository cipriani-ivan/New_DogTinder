namespace NewDogTinder.ViewModels;

public class PlaceViewModel
{
	public int PlaceId { get; set; }
	[Required]
	[MaxLength(200)]
	public string Address { get; set; }
}
