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
		[Produces("application/json")]
		public async Task<ActionResult> PostAppointment([FromBody] PostUpdateAppointment postAppointment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
				Logger.LogInformation("Log message in the PostAppointment() method");
				await AppointmentService.InsertAppointment(postAppointment);
				return Created("", null);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error creating new appointment record");
			}
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
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Produces("application/json")]
		public async Task<ActionResult> PatchAppointment([FromBody] PostUpdateAppointment patchAppointment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
				Logger.LogInformation("Log message in the PostAppointment() method");
				await AppointmentService.UpdateAppointment(patchAppointment);
				return Created("", null);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error creating new appointment record");
			}
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
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[Produces("application/json")]
		public async Task<ActionResult> DeleteAppointment(int appointmentId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
				Logger.LogInformation("Log message in the PostAppointment() method");
				await AppointmentService.DeleteAppointment(appointmentId);
				return Ok();
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error creating new appointment record");
			}
		}
	}
}
