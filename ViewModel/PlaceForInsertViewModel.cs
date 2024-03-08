namespace NewDogTinder.ViewModels;

public class PlaceForInsertViewModel
{
	[Required]
	[MaxLength(200)]
	public string Address { get; set; }
}
