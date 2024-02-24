using NewDogTinder.EFDataAccessLibrary.Models;

namespace NewDogTinder.Repository.IRepositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
	{
		Task<IEnumerable<Appointment>> GetAll();
		void Insert(Appointment owner);
		void Update(Appointment owner);
		void Delete(int appointmentId);
	}
}
