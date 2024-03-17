namespace NewDogTinder.Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class PlaceController : ControllerBase
{
	private readonly IPlaceService PlaceService;

	public PlaceController(IPlaceService placeService)
	{
		PlaceService = placeService;
	}

    /// <summary>
    /// Get a specific place.
    /// </summary>
    /// <param name="placeid"></param>
    /// <returns>A place</returns>
    /// <response code="200"></response>
    [HttpGet("{placeId}", Name = "GetPlace")]
    public async Task<ActionResult> GetAppointment(int placeid)
    {
        var place = await PlaceService.GetPlace(placeid);
        if (place == null)
        {
            return NotFound();
        }
        return Ok(place);
    }

    /// <summary>
    /// Get all the places.
    /// </summary>
    /// <returns>A List of place</returns>
    /// <response code="200"></response>
    [HttpGet]
	public async Task<IList<PlaceViewModel>> GetPlaces()
	{
		return await PlaceService.GetPlaces();
	}

    /// <summary>
    /// Creates a place.
    /// </summary>
    /// <param name="postPlace"></param>
    /// <returns>A newly created place</returns>
    /// <remarks>
    /// Sample request:
    ///     POST /Appointment
    ///     {
    ///        "address": "string",
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Return void</response>
    /// <response code="400">Input parameters are not valid</response>
    [HttpPost]
	public async Task<ActionResult> PostPlace(PlaceForInsertViewModel postPlace)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest();
		}

		var placeCreated = await PlaceService.InsertPlace(postPlace);
        return CreatedAtRoute("GetPlace",
		new
		{
        placeId = placeCreated.PlaceId,
		}, placeCreated);
    }
}
