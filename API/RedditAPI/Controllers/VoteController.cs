using Core.Services;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace RedditAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class VoteController : ControllerBase {
	private readonly IVoteCollectionService _voteCollectionService;

	public VoteController(IVoteCollectionService voteCollectionService) {
		_voteCollectionService = voteCollectionService ?? throw new ArgumentNullException(nameof(voteCollectionService));

	}

	// GET: api/<VoteController>
	[HttpGet]
	//[Authorize(Roles = "Administrator")]
	public IActionResult Get() {
		var result = _voteCollectionService.GetVoteDtos();

		if(result == null)
			return NotFound();

		return Ok(result);
	}

	// GET api/<VoteController>/5
	[HttpGet("{id:int}")]
	//[Authorize(Roles = "Administrator")]
	public IActionResult Get([FromRoute] int id) {
		var result = _voteCollectionService.GetVoteDtoById(id);

		if(result == null)
			return NotFound();

		return Ok(result);
	}

	// POST api/<VoteController>
	[HttpPost]
	//[Authorize(Roles = "Administrator")]
	public IActionResult Post([FromBody] VoteDto voteDto) {
		_voteCollectionService.AddVoteDto(voteDto);

		return Ok();
	}

	// PUT api/<VoteController>
	[HttpPut]
	//[Authorize(Roles = "Administrator")]
	public IActionResult Put([FromBody] VoteDto voteDto) {
		_voteCollectionService.UpdateVoteDto(voteDto);

		return Ok();
	}

	// DELETE api/<VoteController>/5
	[HttpDelete("{id:int}")]
	//[Authorize(Roles = "Administrator")]
	public IActionResult Delete([FromRoute] int id) {
		_voteCollectionService.Delete(id);

		return Ok();
	}
}