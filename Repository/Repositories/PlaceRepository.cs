namespace NewDogTinder.Repository.Repositories;

public class PlaceRepository : GenericRepository<Place>, IPlaceRepository
{
    private readonly NewDogTinderContext Context;

    public PlaceRepository(NewDogTinderContext context) : base(context)
	{
        Context = context;
    }

    public async Task<Place> Get(int placeId)
    {
        // TODO: create a custom exception 
        var place = await Context.Places.Where(x => x.PlaceId == placeId).SingleOrDefaultAsync() ??
            throw new Exception($"Place with id = {placeId} is not present in the database");
        return place;
    }
}