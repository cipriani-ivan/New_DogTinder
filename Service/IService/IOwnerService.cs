using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.ViewModels;

namespace NewDogTinder.Services.IService
{
    public interface IOwnerService
	{
        Task<OwnerViewModel> GetOwner(int ownerId);
        Task<IList<OwnerViewModel>> GetOwners();
        Task<Owner> InsertOwner(OwnerForInsertViewModel ownerViewModel);
    }
}