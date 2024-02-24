using NewDogTinder.EFDataAccessLibrary.DataAccess;
using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.Repository.IRepositories;

namespace NewDogTinder.Repository.Repositories
{
	public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
	{
		public PlaceRepository(NewDogTinderContext context) : base(context)
		{
		}
	}
}