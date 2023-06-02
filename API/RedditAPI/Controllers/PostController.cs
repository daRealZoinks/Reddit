using Core.Services;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedditAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PostController : ControllerBase {
	private readonly IPostCollectionService _postCollectionService;

	public PostController(IPostCollectionService postCollectionService) {
		_postCollectionService =
			postCollectionService ?? throw new ArgumentNullException(nameof(postCollectionService));
	}

	// GET: api/<PostController>
	[HttpGet]
	[Authorize(Roles = "Administrator")]
	public IActionResult Get() {
		var result = _postCollectionService.GetPostDtos();

		if(result == null)
			return NotFound();

		return Ok(result);
	}

	// GET api/<PostController>/5
	[HttpGet("{id:int}")]
	[Authorize(Roles = "Administrator")]
	public IActionResult Get([FromRoute] int id) {
		var result = _postCollectionService.GetPostDtoById(id);

		if(result == null)
			return NotFound();

		return Ok(result);
	}

	// POST api/<PostController>
	[HttpPost]
	[Authorize(Roles = "Administrator")]
	public IActionResult Post([FromBody] PostDto postDto) {
		_postCollectionService.AddPostDto(postDto);

		return Ok();
	}

	// PUT api/<PostController>
	[HttpPut]
	[Authorize(Roles = "Administrator")]
	public IActionResult Put([FromBody] PostDto postDto) {
		_postCollectionService.UpdatePostDto(postDto);

		return Ok();
	}

	// DELETE api/<PostController>/5
	[HttpDelete("{id:int}")]
	[Authorize(Roles = "Administrator")]
	public IActionResult Delete([FromRoute] int id) {
		_postCollectionService.DeletePostDto(id);

		return Ok();
	}
}