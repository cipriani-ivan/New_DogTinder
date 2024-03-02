using NewDogTinder.EFDataAccessLibrary.Models;

namespace NewDogTinder.Repository.IRepositories
{
	public interface IDogRepository: IGenericRepository<Dog>
	{
		Task<Dog> Get(int dogId);
		Dog Insert(Dog dog, int ownerId);
    }
}