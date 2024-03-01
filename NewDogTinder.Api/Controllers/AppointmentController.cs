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
		private readonly ILogger Logger;

		public AppointmentController(IAppointmentService appointmentService, ILoggerFactory logFactory)
		{
			AppointmentService = appointmentService;
			Logger = logFactory.CreateLogger<AppointmentController>();
		}

        [HttpGet("{appointmentid}", Name = "GetAppointment")]
        public async Task<AppointmentViewModel> GetAppointment(int appointmentId)
        {
            Logger.LogInformation("Log message in the GetAppointments() method");
            return (await AppointmentService.GetAppointment(appointmentId));
        }

        [HttpGet]
		public async Task<List<AppointmentViewModel>> GetAppointments()
		{
			Logger.LogInformation("Log message in the GetAppointments() method");
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
        /// <response code="400">If the item is null</response>
        [HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> PostAppointment(AppointmentForInsertViewModel postAppointment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			Logger.LogInformation("Log message in the PostAppointment() method");
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
		/// <param name="UpdateAppointment"></param>
		/// <returns>A newly created appointment</returns>
		/// <remarks>
		/// Sample request:
		///     Patch /Appointment
		///     {
		///        "appointment": 1,
		///        "time": "2022-05-20",
		///        "placeId": 1,
		///        "dogId": 1
		///     }
		///
		/// </remarks>
		/// <response code="204">Return void</response>
		/// <response code="400">If the item is null</response>
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult> UpdateAppointment(AppointmentForUpdateViewModel updateAppointment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			Logger.LogInformation("Log message in the PostAppointment() method");
			var appointmentUpdate = await AppointmentService.UpdateAppointment(updateAppointment);
			if(appointmentUpdate != null)
			{
                return NotFound();
            }

			return NoContent();
		}

        /// <summary>
        /// Update an appointment.
        /// </summary>
        /// <param name="patchAppointment"></param>
        /// <returns>A newly created appointment</returns>
        /// <remarks>
        /// Sample request:
        ///     Patch /Appointment
        ///     {
        ///        "appointment": 1,
        ///        "time": "2022-05-20",
        ///        "placeId": 1,
        ///        "dogId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Return void</response>
        /// <response code="400">If the item is null</response>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <param name="appointment"></param>
        /// <returns>A newly created appointment</returns>
        /// <remarks>
        /// Sample request:
        ///     Delete /Appointment
        ///     {
        ///        "appointment": 1,
        ///        "time": "2022-05-20",
        ///        "placeId": 1,
        ///        "dogId": 1
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Return void</response>
        /// <response code="400">If the item is null</response>
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
