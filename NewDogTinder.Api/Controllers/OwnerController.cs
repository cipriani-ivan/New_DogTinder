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

		public OwnerController(IOwnerService ownerService)
		{
			OwnerService = ownerService;
		}

        /// <summary>
        /// Get specific owner.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns>Owner</returns>
        /// <response code="200"></response>
        [HttpGet("{ownerid}", Name = "GetOwner")]
        public async Task<OwnerViewModel> GetOwner(int ownerId)
        {
            return await OwnerService.GetOwner(ownerId);
        }

        /// <summary>
        /// Get all the owners.
        /// </summary>
        /// <param name="ownerId"></param>
        /// <returns>A list of owners</returns>
        /// <response code="200"></response>
        [HttpGet]
		public async Task<IList<OwnerViewModel>> GetOwners()
		{
			return await OwnerService.GetOwners();
		}

        /// <summary>
        /// Creates an owner.
        /// </summary>
        /// <param name="ownerViewModel"></param>
        /// <returns>A newly created appointment</returns>
        /// <remarks>
        /// Sample request:
        ///     POST /Owner
        ///     {
        ///        "name": "string",
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return void</response>
        /// <response code="400">Input parameters are not valid</response>
        [HttpPost]
		public async Task<ActionResult> PostOwner(OwnerForInsertViewModel ownerViewModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var ownerCreated = await OwnerService.InsertOwner(ownerViewModel);
            return CreatedAtRoute("GetOwner",
                new
                {
                    ownerId = ownerCreated.OwnerId,
                }, ownerCreated);
        }		
	}
}
