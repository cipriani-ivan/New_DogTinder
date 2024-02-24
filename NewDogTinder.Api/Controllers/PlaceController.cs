using Microsoft.AspNetCore.Mvc;
using NewDogTinder.Services.IService;
using NewDogTinder.ViewModels;

namespace NewNewDogTinder.Api.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class PlaceController : ControllerBase
	{
		private readonly IPlaceService PlaceService;
		private readonly ILogger Logger;

		public PlaceController(IPlaceService placeService, ILoggerFactory logFactory)
		{
			PlaceService = placeService;
			Logger = logFactory.CreateLogger<AppointmentController>();
		}

		[HttpGet]
		public async Task<IList<PlaceViewModel>> GetPlaces()
		{
			return await PlaceService.GetPlaces();
		}

		[HttpPost]
		public async Task<ActionResult> PostPlace([FromBody] PlaceViewModel placeViewModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
				Logger.LogInformation("Log message in the PostPlace() method");
				await PlaceService.InsertPlace(placeViewModel);
				return Created("", null);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error creating new place record");
			}
		}
	}
}
