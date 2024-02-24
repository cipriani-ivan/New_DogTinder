using System.ComponentModel.DataAnnotations;

namespace NewDogTinder.EFDataAccessLibrary.Models
{
	public class Owner
	{
		public int OwnerId { get; set; }
		[Required]
		[MaxLength(200)]
		public string Name { get; set; }
	}
}
