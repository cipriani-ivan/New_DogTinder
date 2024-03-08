namespace NewDogTinder.ViewModels;

public class OwnerViewModel
{
	public int OwnerId { get; set; }
	[Required]
	[MaxLength(200)]
	public string Name { get; set; }
}
