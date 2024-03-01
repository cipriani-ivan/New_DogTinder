using System.ComponentModel.DataAnnotations;

namespace NewDogTinder.ViewModels
{
	public class OwnerForInsertViewModel
	{
		[Required]
		[MaxLength(200)]
		public string Name { get; set; }
	}
}
