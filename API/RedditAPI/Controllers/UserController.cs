using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedditAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserCollectionService _userCollectionService;

    public UserController(IUserCollectionService userCollectionService,
        IMessageCollectionService messageCollectionService)
    {
        _ = messageCollectionService.GetAll(); // very temporary, find other way to initialize the message database indirectly
        _userCollectionService =
            userCollectionService ?? throw new ArgumentNullException(nameof(userCollectionService));
    }

    // GET: api/<UserController>
    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public IActionResult Get()
    {
        var result = _userCollectionService.GetUserDtos();

        if (result == null) return NotFound();

        return Ok(result);
    }

    // GET api/<UserController>/5
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public IActionResult Get([FromRoute] int id)
    {
        var result = _userCollectionService.GetUserDtoById(id);

        if (result == null) return NotFound();

        return Ok(result);
    }

    // POST api/<UserController>
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public IActionResult Post([FromBody] UserPayloadDto payload)
    {
        _userCollectionService.AddUserDto(payload);

        return Ok();
    }

    // PUT api/<UserController>
    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public IActionResult Put([FromBody] UserPayloadDto payload)
    {
        _userCollectionService.UpdateUserDto(payload);

        return Ok();
    }

    // DELETE api/<UserController>/5
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public IActionResult Delete([FromRoute] int id)
    {
        _userCollectionService.Delete(id);

        return Ok();
    }

    // POST api/<UserController>/register
    [HttpPost("register")]
    [AllowAnonymous]
    public IActionResult Register(RegisterDto payload)
    {
        var result = _userCollectionService.Register(payload);

        if (result == null) return BadRequest("User cannot be registered");

        return Ok(result);
    }

    // POST api/<UserController>/login
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginDto payload)
    {
        var result = _userCollectionService.Login(payload);

        if (result == null) return NotFound("Invalid credentials");

        return Ok(result);
    }
}