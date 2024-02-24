using System.ComponentModel.DataAnnotations;

namespace NewDogTinder.ViewModels
{
	public class DogViewModel
	{
		public int DogId { get; set; }
		[Required]
		[MaxLength(200)]
		public string Name { get; set; }
		[Required]
		[MaxLength(200)]
		public string Breed { get; set; }
		[Required]
		public int  OwnerId { get; set; }
		public OwnerViewModel Owner { get; set; }
	}
}
