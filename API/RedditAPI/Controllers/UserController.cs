using Core.Services;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedditAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserCollectionService _userCollectionService;

    public UserController(IUserCollectionService userCollectionService)
    {
        _userCollectionService =
            userCollectionService ?? throw new ArgumentNullException(nameof(userCollectionService));
    }

    // GET: api/<UserController>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_userCollectionService.GetAll());
    }

    // GET api/<UserController>/5
    [HttpGet("{id:int}")]
    public IActionResult Get([FromRoute] int id)
    {
        return Ok(_userCollectionService.GetById(id));
    }

    // POST api/<UserController>
    [HttpPost]
    public IActionResult Post([FromBody] User user)
    {
        _userCollectionService.Add(user);

        return Ok();
    }

    // PUT api/<UserController>
    [HttpPut]
    public IActionResult Put([FromBody] User user)
    {
        _userCollectionService.Update(user);

        return Ok();
    }

    // DELETE api/<UserController>/5
    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        _userCollectionService.Delete(id);

        return Ok();
    }
}