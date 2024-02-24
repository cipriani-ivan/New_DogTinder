using System.Collections.Generic;
using System.Threading.Tasks;
using NewDogTinder.ViewModels;

namespace NewDogTinder.Services.IService
{
	public interface IAppointmentService
	{
		Task<IList<AppointmentViewModel>> GetAppointments();
		Task InsertAppointment(PostUpdateAppointment appointmentViewModel);
		Task UpdateAppointment(PostUpdateAppointment appointmentViewModel);
		Task DeleteAppointment(int appointmentId);
	}
}
