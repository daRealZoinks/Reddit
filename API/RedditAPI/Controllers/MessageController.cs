using Core.Dtos;
using Core.Services;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedditAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController : ControllerBase
{
    private readonly IMessageCollectionService _messageCollectionService;

    public MessageController(IMessageCollectionService messageCollectionService)
    {
        _messageCollectionService = messageCollectionService ??
                                    throw new ArgumentNullException(nameof(messageCollectionService));
    }

    // GET: api/<MessageController>
    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public IActionResult Get()
    {
        var result = _messageCollectionService.GetMessageDtos();

        if (result == null) return NotFound();

        return Ok(result);
    }

    // GET api/<MessageController>/5
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public IActionResult Get([FromRoute] int id)
    {
        var result = _messageCollectionService.GetMessageDtoById(id);

        if (result == null) return NotFound();

        return Ok(result);
    }

    // POST api/<MessageController>
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public IActionResult Post([FromBody] PostMessageDto postMessageDto)
    {
        _messageCollectionService.AddMessageDto(postMessageDto);

        return Ok();
    }

    // PUT api/<MessageController>
    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public IActionResult Put([FromBody] MessageDto messageDto)
    {
        _messageCollectionService.UpdateMessageDto(messageDto);

        return Ok();
    }

    // DELETE api/<MessageController>/5
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public IActionResult Delete([FromRoute] int id)
    {
        _messageCollectionService.Delete(id);

        return Ok();
    }
}