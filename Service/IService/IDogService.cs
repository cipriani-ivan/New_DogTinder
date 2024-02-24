using System.Collections.Generic;
using System.Threading.Tasks;
using NewDogTinder.ViewModels;

namespace NewDogTinder.Services.IService
{
	public interface IDogService
	{
		Task<IList<DogViewModel>> GetDogs();
		Task InsertDog(DogViewModel dogViewmodel);
	}
}