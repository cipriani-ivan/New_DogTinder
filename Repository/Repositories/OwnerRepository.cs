using NewDogTinder.Repository;

namespace DogTinder.Repository.Repositories;

public class OwnerRepository : GenericRepository<Owner>, IOwnerRepository
{
    private readonly NewDogTinderContext Context;

    public OwnerRepository(NewDogTinderContext context) : base(context)
    {
        Context = context;
    }

    public async Task<Owner> Get(int ownerId)
    {
        // TODO: create a custom exception 
        var owner = await Context.Owners.Where(x => x.OwnerId == ownerId).SingleOrDefaultAsync() ??
            throw new Exception($"Owner with id = {ownerId} is not present in the database");
        return owner;
    }
}