﻿namespace NewDogTinder.Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class DogController : ControllerBase
{
	private readonly IDogService DogService;

	public DogController(IDogService ownerService)
	{
		DogService = ownerService;
	}

    /// <summary>
    /// Get a specific dog with the owner.
    /// </summary>
    /// <param name="dogId"></param>
    /// <returns>A specific dog with the owner</returns>
    /// <response code="200"></response>
    [HttpGet("{dogId}", Name = "GetDog")]
    public async Task<ActionResult> GetDog(int dogId)
    {
        var dog =  await DogService.GetDog(dogId);
        if (dog == null)
        {
            return NotFound();
        }
        return Ok(dog);
    }

    /// <summary>
    /// Get all dogs included owner.
    /// </summary>
    /// <returns>A list of dogs with the owner</returns>
    /// <response code="200"></response>
    [HttpGet]
	public async Task<IList<DogViewModel>> GetDogs()
	{
		return await DogService.GetDogs();
    }

    /// <summary>
    /// Creates a dog.
    /// </summary>
    /// <param name="postDog"></param>
    /// <returns>A newly created dog</returns>
    /// <remarks>
    /// Sample request:
    ///     POST /Dog
    ///     {
    ///        "name": "string",
    ///        "breed": "string",
    ///        "ownerId": 1
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Return void</response>
    /// <response code="400">Input parameters are not valid</response>
    [HttpPost]
	public async Task<ActionResult> PostDog(DogForInsertViewModel postDog)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest();
		}

		var dogCreated = await DogService.InsertDog(postDog);
        return CreatedAtRoute("GetDog",
		new
		{
        dogId = dogCreated.DogId,
		}, dogCreated);
    }
}
