using System.Collections.Generic;
using System.Threading.Tasks;
using NewDogTinder.ViewModels;

namespace NewDogTinder.Services.IService
{
	public interface IPlaceService
	{
		Task<IList<PlaceViewModel>> GetPlaces();
		Task InsertPlace(PlaceViewModel placeViewmodel);
	}
}