namespace NewDogTinder.Services.IService;

public interface IPlaceService
{
	Task<PlaceViewModel> GetPlace(int placeId);
    Task<IList<PlaceViewModel>> GetPlaces();
	Task<Place> InsertPlace(PlaceForInsertViewModel placeViewmodel);
}