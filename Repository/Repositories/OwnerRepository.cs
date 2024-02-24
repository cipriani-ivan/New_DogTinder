using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.Repository.IRepositories;
using NewDogTinder.Repository;
using NewDogTinder.EFDataAccessLibrary.DataAccess;

namespace DogTinder.Repository.Repositories
{
    public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
    {
        public OwnerRepository(NewDogTinderContext context) : base(context)
        {
        }
    }
}