using Core.Services;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedditAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentController : ControllerBase
{
    private readonly ICommentCollectionService _commentCollectionService;

    public CommentController(ICommentCollectionService commentCollectionService)
    {
        _commentCollectionService = commentCollectionService ??
                                        throw new ArgumentNullException(nameof(commentCollectionService));
    }

    // GET: api/<CommentController>
    [HttpGet]
    //[Authorize(Roles = "Administrator")]
    public IActionResult Get()
    {
        var result = _commentCollectionService.GetCommentDtos();

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // GET api/<CommentController>/5
    [HttpGet("{id:int}")]
    //[Authorize(Roles = "Administrator")]
    public IActionResult Get([FromRoute] int id)
    {
        var result = _commentCollectionService.GetCommentDtoById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // POST api/<CommentController>
    [HttpPost]
    //[Authorize(Roles = "Administrator")]
    public IActionResult Post([FromBody] CommentDto commentDto)
    {
        _commentCollectionService.AddCommentDto(commentDto);

        return Ok();
    }

    // PUT api/<CommentController>
    [HttpPut]
    //[Authorize(Roles = "Administrator")]
    public IActionResult Put([FromBody] CommentDto commentDto)
    {
        _commentCollectionService.UpdateCommentDto(commentDto);

        return Ok();
    }

    // DELETE api/<CommentController>/5
    [HttpDelete("{id:int}")]
    //[Authorize(Roles = "Administrator")]
    public IActionResult Delete([FromRoute] int id)
    {
        _commentCollectionService.Delete(id);

        return Ok();
    }
}