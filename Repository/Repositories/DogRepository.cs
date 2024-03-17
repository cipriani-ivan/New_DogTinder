namespace NewDogTinder.Repository.Repositories;

public class DogRepository : GenericRepository<Dog>, IDogRepository
{
	private readonly NewDogTinderContext Context;
	private readonly ILogger Logger;

	public DogRepository(NewDogTinderContext context, ILoggerFactory logFactory) : base(context)
	{
		Context = context;
		Logger = logFactory.CreateLogger<AppointmentRepository>();
	}

    public async Task<Dog> Get(int dogId)
    {
        try
        {
            var dog = await Context.Dogs.Where(x => x.DogId == dogId).Include(a => a.Owner).SingleOrDefaultAsync();
            return dog;
        }
        catch { return null; }
    }

    public Dog Insert(Dog dog, int ownerId)
	{
		var dogCreated = Context.Dogs.Add(dog).Entity;
		try
		{
			var owner = Context.Owners.Single(x => x.OwnerId == ownerId);
                dogCreated.Owner = owner;
			return dogCreated;
            }
		catch
		{
			Logger.LogInformation($"Log message in the Insert() method OwnerId = {ownerId} is not a valid id");
			throw;
		}
	}
}