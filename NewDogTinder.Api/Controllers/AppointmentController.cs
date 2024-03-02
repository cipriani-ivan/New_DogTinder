using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NewDogTinder.Services.IService;
using NewDogTinder.ViewModels;

namespace NewNewDogTinder.Api.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class AppointmentController : ControllerBase
	{
		private readonly IAppointmentService AppointmentService;

		public AppointmentController(IAppointmentService appointmentService)
		{
			AppointmentService = appointmentService;
		}

        /// <summary>
        /// Get a specific appointment.
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns>An appointment</returns>
        /// <response code="200"></response>
        [HttpGet("{appointmentid}", Name = "GetAppointment")]
        public async Task<AppointmentViewModel> GetAppointment(int appointmentId)
        {
            return await AppointmentService.GetAppointment(appointmentId);
        }

        /// <summary>
        /// Get all appointments.
        /// </summary>
        /// <returns>A list of appointments</returns>
        /// <response code="200"></response>
        [HttpGet]
		public async Task<List<AppointmentViewModel>> GetAppointments()
		{
			return (await AppointmentService.GetAppointments()).ToList();
		}

        /// <summary>
        /// Creates an appointment.
        /// </summary>
        /// <param name="postAppointment"></param>
        /// <returns>A newly created appointment</returns>
        /// <remarks>
        /// Sample request:
        ///     POST /Appointment
        ///     {
        ///        "time": "2022-05-20",
        ///        "placeId": 1,
        ///        "dogId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return void</response>
        /// <response code="400">Input parameters are not valid</response>
        [HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> PostAppointment(AppointmentForInsertViewModel postAppointment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var appointmentCreated = await AppointmentService.InsertAppointment(postAppointment);
			return CreatedAtRoute("GetAppointment",
                new
                {
                    appointmentId = appointmentCreated.AppointmentId,
                }, appointmentCreated);
		}

        /// <summary>
        /// Update an appointment.
        /// </summary>
        /// <param name="updateAppointment"></param>
        /// <remarks>
        /// Sample request:
        ///     Put /Appointment
        ///     {
        ///        "appointment": 1,
        ///        "time": "2022-05-20",
        ///        "placeId": 1,
        ///        "dogId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Return void</response>
        /// <response code="400">Input parameters are not valid</response>
        [HttpPut]
		public async Task<ActionResult> UpdateAppointment(AppointmentForUpdateViewModel updateAppointment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var appointmentUpdate = await AppointmentService.UpdateAppointment(updateAppointment);
			if(appointmentUpdate != null)
			{
                return NotFound();
            }

			return NoContent();
		}

        /// <summary>
        /// Update partially an appointment.
        /// </summary>
        /// <param name="patchAppointment"></param>
        /// <param name="appointmentId"></param>
        /// <remarks>
        /// Sample request:
        ///     Patch /Appointment
        ///     {
        ///        "time": "2022-05-20",
        ///        "placeId": 1,
        ///        "dogId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Return void</response>
        /// <response code="400">Input parameters are not valid</response>
        /// <response code="404">Not found</response>
        [HttpPatch]
        public async Task<ActionResult> PatchAppointment(int appointmentId, JsonPatchDocument<AppointmentForUpdateViewModel> patchAppointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var appointmentUpdate = await AppointmentService.PartiallyUpdateAppointment(appointmentId, patchAppointment);
            if (appointmentUpdate == null)
            {
                return NotFound();
            }

            if (!TryValidateModel(appointmentUpdate))
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        /// <summary>
        /// Update an appointment.
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <response code="200">Return void</response>
        /// <response code="404">Not found</response>
        [HttpDelete]
		[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAppointment(int appointmentId)
		{
            if (await AppointmentService.DeleteAppointment(appointmentId)) 
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
		}
	}
}
