using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using NewDogTinder.EFDataAccessLibrary.Models;
using NewDogTinder.Repository.IRepositories;
using NewDogTinder.Services.IService;
using NewDogTinder.ViewModels;

namespace NewDogTinder.Services.Service
{
    public class AppointmentService: IAppointmentService
	{
		private IAppointmentRepository AppointmentRepository { get; }
		private readonly IMapper Mapper;

		public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper)
		{
			AppointmentRepository = appointmentRepository;
			Mapper = mapper;
		}

		public async Task<AppointmentViewModel> GetAppointment(int appointmentId)
		{
			var appointment = await AppointmentRepository.Get(appointmentId);
			return Mapper.Map<AppointmentViewModel>(appointment);
		}

        public async Task<IList<AppointmentViewModel>> GetAppointments()
        {
            var appointments = await AppointmentRepository.GetAll();
            return Mapper.Map<List<AppointmentViewModel>>(appointments);
        }

        public async Task<Appointment> InsertAppointment(AppointmentForInsertViewModel appointmentViewModel)
		{
            var appointment = Mapper.Map<Appointment>(appointmentViewModel);
            var appointmentCreated = AppointmentRepository.InsertAppointment(appointment);
			await AppointmentRepository.SaveAsync();
			return appointmentCreated;
        }

		public async Task<Appointment> UpdateAppointment(AppointmentForUpdateViewModel appointmentViewModel)
		{
            var appointment = Mapper.Map<Appointment>(appointmentViewModel);
            var appointmentUpdate = AppointmentRepository.UpdateAppointment(appointment);
			await AppointmentRepository.SaveAsync();
            return appointmentUpdate;
        }

        public async Task<Appointment> PartiallyUpdateAppointment(int appointmentId, JsonPatchDocument<AppointmentForUpdateViewModel> appointmentPatch)
        {
            var appointment = await AppointmentRepository.Get(appointmentId);
            if (appointment == null)
            {
                return null;
            }
            var appointmentForUpdateViewModel = Mapper.Map<AppointmentForUpdateViewModel>(appointment);
            appointmentPatch.ApplyTo(appointmentForUpdateViewModel);
            Mapper.Map(appointmentForUpdateViewModel, appointment);
            var appointmentPartiallyUpdate = AppointmentRepository.UpdateAppointment(appointment);

            await AppointmentRepository.SaveAsync();
            return appointmentPartiallyUpdate;
        }

        public async Task<bool> DeleteAppointment(int appointmentId)
		{
			var successDelete = AppointmentRepository.Delete(appointmentId);

			await AppointmentRepository.SaveAsync();
            return successDelete;
		}
	}
}
