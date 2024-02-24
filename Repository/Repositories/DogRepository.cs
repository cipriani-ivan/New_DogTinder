using Microsoft.Extensions.Logging;
using NewDogTinder.EFDataAccessLibrary.DataAccess;
using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.Repository.IRepositories;

namespace NewDogTinder.Repository.Repositories
{
    public class DogRepository : GenericRepository<Dog>, IDogRepository
	{
		private readonly NewDogTinderContext Context;
		private readonly ILogger Logger;

		public DogRepository(NewDogTinderContext context, ILoggerFactory logFactory) : base(context)
		{
			Context = context;
			Logger = logFactory.CreateLogger<AppointmentRepository>();
		}

		public void Insert(Dog dog, int ownerId)
		{
			var d = Context.Dogs.Add(dog);
			try
			{
				var owner = Context.Owners.First(x => x.OwnerId == ownerId);
				d.Entity.Owner = owner;
			}
			catch
			{
				Logger.LogInformation($"Log message in the Insert() method OwnerId = {ownerId} is not a valid id");
				throw;
			}
		}
	}
}