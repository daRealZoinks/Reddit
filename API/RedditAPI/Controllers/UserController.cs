using Core.Dtos;
using Core.Services;
using DataLayer.Entities;
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

    public UserController(IUserCollectionService userCollectionService)
    {
        _userCollectionService =
            userCollectionService ?? throw new ArgumentNullException(nameof(userCollectionService));
    }

    // GET: api/<UserController>
    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public IActionResult Get()
    {
        return Ok(_userCollectionService.GetAll());
    }

    // GET api/<UserController>/5
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public IActionResult Get([FromRoute] int id)
    {
        return Ok(_userCollectionService.GetById(id));
    }

    // POST api/<UserController>
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public IActionResult Post([FromBody] User user)
    {
        _userCollectionService.Add(user);

        return Ok();
    }

    // PUT api/<UserController>
    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public IActionResult Put([FromBody] User user)
    {
        _userCollectionService.Update(user);

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

        if (result == null)
        {
            return BadRequest("User cannot be registered");
        }

        return Ok(result);
    }

    // POST api/<UserController>/login
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginDto payload)
    {
        var result = _userCollectionService.Login(payload);

        if (result == null)
        {
            return BadRequest("Invalid credentials");
        }

        return Ok(result);
    }
}