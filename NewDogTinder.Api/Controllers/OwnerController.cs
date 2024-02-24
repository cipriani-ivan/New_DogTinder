using Microsoft.AspNetCore.Mvc;
using NewDogTinder.Services.IService;
using NewDogTinder.ViewModels;

namespace NewNewDogTinder.Api.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class OwnerController : ControllerBase
	{
		private readonly IOwnerService OwnerService;
		private readonly ILogger Logger;

		public OwnerController(IOwnerService ownerService, ILoggerFactory logFactory)
		{
			OwnerService = ownerService;
			Logger = logFactory.CreateLogger<AppointmentController>();
		}

		[HttpGet]
		public async Task<IList<OwnerViewModel>> GetOwners()
		{
			return await OwnerService.GetOwners();
		}

		[HttpPost]
		public async Task<ActionResult> PostOwner([FromBody] OwnerViewModel ownerViewModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			try
			{
				Logger.LogInformation("Log message in the PostOwner() method");
				await OwnerService.InsertOwner(ownerViewModel);
				return Created("", null);
			}
			catch (Exception)
			{
				return StatusCode(StatusCodes.Status500InternalServerError,
					"Error creating new owner record");
			}
		}		
	}
}
