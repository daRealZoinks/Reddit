using Core.Services;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedditAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase {
	private readonly IPostCollectionService _postCollectionService;

	public PostController(IPostCollectionService postCollectionService) {
		_postCollectionService =
			postCollectionService ?? throw new ArgumentNullException(nameof(postCollectionService));
	}

	// GET: api/<PostController>
	[HttpGet]
	public IActionResult Get() {
		var result = _postCollectionService.GetPostDtos();

		if(result == null)
			return NotFound();

		return Ok(result);
	}

	// GET api/<PostController>/5
	[HttpGet("{id:int}")]
	public IActionResult Get([FromRoute] int id) {
		var result = _postCollectionService.GetPostDtoById(id);

		if(result == null)
			return NotFound();

		return Ok(result);
	}

	// POST api/<PostController>
	[HttpPost]
	public IActionResult Post([FromBody] PostDto postDto) {
		_postCollectionService.AddPostDto(postDto);

		return Ok();
	}

	// PUT api/<PostController>
	[HttpPut]
	public IActionResult Put([FromBody] PostDto postDto) {
		_postCollectionService.UpdatePostDto(postDto);

		return Ok();
	}

	// DELETE api/<PostController>/5
	[HttpDelete("{id:int}")]
	public IActionResult Delete([FromRoute] int id) {
		_postCollectionService.DeletePostDto(id);

		return Ok();
	}
}