using NewDogTinder.EFDataAccessLibrary.Models;

namespace NewDogTinder.Repository.IRepositories
{
	public interface IDogRepository: IGenericRepository<Dog>
	{
		void Insert(Dog dog, int ownerId);
	}
}