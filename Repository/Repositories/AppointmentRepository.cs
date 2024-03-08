using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace NewDogTinder.Repository.Repositories;

public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{

	private readonly NewDogTinderContext Context;
	private readonly ILogger Logger;

	public AppointmentRepository(NewDogTinderContext context, ILoggerFactory logFactory) : base(context)
	{
		Context = context;
		Logger = logFactory.CreateLogger<AppointmentRepository>();
	}

    public async Task<Appointment> Get(int appointmentId)
    {
	// TODO: create a custom exception 
	var appointment = await Context.Appointments.Where(x => x.AppointmentId == appointmentId)
		.Include(a => a.Place).Include(a => a.Dog).ThenInclude(a => a.Owner).SingleOrDefaultAsync() ??
		throw new Exception($"Appointment with id = {appointmentId} is not present in the database");
	return appointment;
    }

    public async Task<IEnumerable<Appointment>> GetAll()
	{
		return await Context.Appointments.Include(a => a.Place).Include(a => a.Dog).ThenInclude(a => a.Owner).ToListAsync();
	}

	public Appointment InsertAppointment(Appointment appointment)
	{
		try
		{
			var dog = Context.Dogs.First(x => x.DogId == appointment.Dog.DogId);
			var place = Context.Places.First(x => x.PlaceId == appointment.Place.PlaceId);
			appointment.Dog = dog;
			appointment.Place = place;
			return Context.Appointments.Add(appointment).Entity;
		}
		catch
		{
			Logger.LogInformation($"Log message in the Insert() method dogId = {appointment.Dog.DogId} or placeId = {appointment.Place.PlaceId} is not a valid id");
			throw;
		}
	}

	public Appointment UpdateAppointment(Appointment appointment)
	{
		try
		{
			var app = Context.Appointments.FirstOrDefault(i => i.AppointmentId == appointment.AppointmentId);
			
			var dog = Context.Dogs.First(x => x.DogId == appointment.Dog.DogId);
			var place = Context.Places.First(x => x.PlaceId == appointment.Place.PlaceId);

			if (app == null)
			{
				return null;
			}

			app.Dog = dog;
			app.Place = place;
			app.Time = appointment.Time;
			return app;

            }
		catch(Exception)
		{
			Logger.LogInformation($"Log message in the Update() method dogId = {appointment.Dog.DogId} or placeId = {appointment.Place.PlaceId}  or is not a valid id");
			throw;
		}
	}

	public bool Delete(int appointmentId)
	{
		try
		{
			var app = Context.Appointments.FirstOrDefault(i => i.AppointmentId == appointmentId);
			if (app != null)
			{
				Context.Appointments.Remove(app);
				return true;
			}
			else 
			{ 
				return false; 
			}
		}
		catch (Exception)
		{
			Logger.LogInformation($"Log message in the Delete() method appointmentId = {appointmentId} is not a valid id");
			throw;
		}
	}
}

