using System.Collections.Generic;
using System.Threading.Tasks;
using NewDogTinder.ViewModels;

namespace NewDogTinder.Services.IService
{
	public interface IOwnerService
	{
		Task<IList<OwnerViewModel>> GetOwners();
		Task InsertOwner(OwnerViewModel ownerViewmodel);
	}
}