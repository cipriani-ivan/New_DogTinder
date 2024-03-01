using NewDogTinder.EFDataAccessLibrary.Models;

namespace NewDogTinder.Repository.IRepositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
	{
		Task<Appointment> Get(int appointmentId);
        Task<IEnumerable<Appointment>> GetAll();
        Appointment InsertAppointment(Appointment owner);
        Appointment UpdateAppointment(Appointment owner);
		bool Delete(int appointmentId);
	}
}
