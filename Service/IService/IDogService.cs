using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.ViewModels;

namespace NewDogTinder.Services.IService
{
    public interface IDogService
	{
		Task<DogViewModel> GetDog(int dogId);
        Task<IList<DogViewModel>> GetDogs();
        Task<Dog> InsertDog(DogForInsertViewModel dogViewmodel);
    }
}