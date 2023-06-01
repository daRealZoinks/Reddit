using Core.Services;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedditAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommunityController : ControllerBase {
	private readonly ICommunityCollectionService _communityCollectionService;

	public CommunityController(ICommunityCollectionService communityCollectionService) {
		_communityCollectionService =
			communityCollectionService ?? throw new ArgumentNullException(nameof(communityCollectionService));
	}

	// GET: api/<CommunityController>
	[HttpGet]
	[Authorize(Roles = "Administrator")]
	public IActionResult Get() {
		var result = _communityCollectionService.GetCommunityDtos();

		if(result == null)
			return NotFound();

		return Ok(result);
	}

	// GET api/<CommunityController>/5
	[HttpGet("{id:int}")]
	[Authorize(Roles = "Administrator")]
	public IActionResult Get([FromRoute] int id) {
		var result = _communityCollectionService.GetCommunityDtoById(id);

		if(result == null)
			return NotFound();

		return Ok(result);
	}

	// POST api/<CommunityController>
	[HttpPost]
	[Authorize(Roles = "Administrator")]
	public IActionResult Post([FromBody] CommunityDto communityDto) {
		_communityCollectionService.AddCommunityDto(communityDto);

		return Ok();
	}
}