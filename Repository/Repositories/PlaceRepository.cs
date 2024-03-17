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
        try
        {
            var place = await Context.Places.Where(x => x.PlaceId == placeId).SingleOrDefaultAsync();
            return place;
        }
        catch { return null; }
    }
}