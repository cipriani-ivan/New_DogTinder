using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using NewDogTinder.EFDataAccessLibrary.DataAccess;
using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.Repository.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace NewDogTinder.Repository.Repositories
{
	public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
	{

		private readonly NewDogTinderContext Context;
		private readonly ILogger Logger;

		public AppointmentRepository(NewDogTinderContext context, ILoggerFactory logFactory) : base(context)
		{
			Context = context;
			Logger = logFactory.CreateLogger<AppointmentRepository>();
		}

		public async Task<IEnumerable<Appointment>> GetAll()
		{
			return await Context.Appointments.Include(a => a.Place).Include(a => a.Dog).ThenInclude(a => a.Owner).ToListAsync();
		}

		public override void Insert(Appointment appointment)
		{
			try
			{
				var dog = Context.Dogs.First(x => x.DogId == appointment.Dog.DogId);
				var place = Context.Places.First(x => x.PlaceId == appointment.Place.PlaceId);
				appointment.Dog = dog;
				appointment.Place = place;
				Context.Appointments.Add(appointment);
			}
			catch
			{
				Logger.LogInformation($"Log message in the Insert() method dogId = {appointment.Dog.DogId} or placeId = {appointment.Place.PlaceId} is not a valid id");
				throw;
			}
		}

		public override void Update(Appointment appointment)
		{
			try
			{
				var app = Context.Appointments.FirstOrDefault(i => i.AppointmentId == appointment.AppointmentId);
				
				var dog = Context.Dogs.First(x => x.DogId == appointment.Dog.DogId);
				var place = Context.Places.First(x => x.PlaceId == appointment.Place.PlaceId);

				if (app == null) return;
				app.Dog = dog;
				app.Place = place;
				app.Time = appointment.Time;
			}
			catch(Exception)
			{
				Logger.LogInformation($"Log message in the Update() method dogId = {appointment.Dog.DogId} or placeId = {appointment.Place.PlaceId}  or is not a valid id");
				throw;
			}
		}

		public void Delete(int appointmentId)
		{
			try
			{
				var app = Context.Appointments.FirstOrDefault(i => i.AppointmentId == appointmentId);
				if (app != null) Context.Appointments.Remove(app);
			}
			catch (Exception)
			{
				Logger.LogInformation($"Log message in the Delete() method appointmentId = {appointmentId} is not a valid id");
				throw;
			}
		}
	}
}

