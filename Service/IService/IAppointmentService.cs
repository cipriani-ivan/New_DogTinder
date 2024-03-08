namespace NewDogTinder.Services.IService;

public interface IAppointmentService
{
	Task<AppointmentViewModel> GetAppointment(int appointmentId);
	Task<IList<AppointmentViewModel>> GetAppointments();
	Task<Appointment> InsertAppointment(AppointmentForInsertViewModel appointmentViewModel);
	Task<Appointment> UpdateAppointment(AppointmentForUpdateViewModel appointmentViewModel);
	Task<Appointment> PartiallyUpdateAppointment(int appointmentId, JsonPatchDocument<AppointmentForUpdateViewModel> appointmentPatch);
	Task<bool> DeleteAppointment(int appointmentId);
}
