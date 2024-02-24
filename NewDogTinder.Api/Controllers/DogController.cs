using Microsoft.AspNetCore.Mvc;
using NewDogTinder.Services.IService;
using NewDogTinder.ViewModels;

namespace NewNewDogTinder.Api.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class DogController : ControllerBase
	{
		private readonly IDogService DogService;
		private readonly ILogger Logger;

		public DogController(IDogService ownerService, ILoggerFactory logFactory)
		{
			DogService = ownerService;
			Logger = logFactory.CreateLogger<AppointmentController>();
		}

		[HttpGet]
		public async Task<IList<DogViewModel>> GetDogs()
		{
			return await DogService.GetDogs();
		}

		[HttpPost]
		public async Task<ActionResult> PostDog([FromBody] DogViewModel dogViewModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
				Logger.LogInformation("Log message in the PostDog() method");
				await DogService.InsertDog(dogViewModel);
				return Created("", null);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error creating new dog record");
			}
		}
	}
}
