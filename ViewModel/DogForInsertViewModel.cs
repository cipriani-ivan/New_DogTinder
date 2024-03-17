namespace NewDogTinder.ViewModels;

public class DogForInsertViewModel
{
	[Required]
	[MaxLength(200)]
	public string Name { get; set; }
	[Required]
	[MaxLength(200)]
	public string Breed { get; set; }
	[Required]
    [Range(1, int.MaxValue)]
    public int  OwnerId { get; set; }
}
