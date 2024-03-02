using NewDogTinder.EFDataAccessLibrary.Models;

namespace NewDogTinder.Repository.IRepositories
{
	public interface IPlaceRepository: IGenericRepository<Place>
	{
        Task<Place> Get(int placeId);
    }
}