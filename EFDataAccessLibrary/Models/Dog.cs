using System.ComponentModel.DataAnnotations;

namespace NewDogTinder.EFDataAccessLibrary.Models
{
    public class Dog
	{
		public int DogId { get; set; }
		[Required]
		[MaxLength(200)]
		public string Name { get; set; }
		[Required]
		[MaxLength(200)]
		public string Breed { get; set; }	
		
		public Owner Owner { get; set; }
	}
}
