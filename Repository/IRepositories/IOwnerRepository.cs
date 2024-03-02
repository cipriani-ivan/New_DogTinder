using NewDogTinder.EFDataAccessLibrary.Models;

namespace NewDogTinder.Repository.IRepositories
{
	public interface IOwnerRepository: IGenericRepository<Owner>
	{
        Task<Owner> Get(int ownerId);
    }
}