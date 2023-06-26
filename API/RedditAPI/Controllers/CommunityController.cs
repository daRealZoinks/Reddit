using Core.Services;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedditAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommunityController : ControllerBase
{
    private readonly ICommunityCollectionService _communityCollectionService;
    private readonly IUserCollectionService _userCollectionService;

    public CommunityController(ICommunityCollectionService communityCollectionService, IUserCollectionService userCollectionService)
    {
        _communityCollectionService = communityCollectionService ?? throw new ArgumentNullException(nameof(communityCollectionService));
        _userCollectionService = userCollectionService ?? throw new ArgumentNullException(nameof(userCollectionService));
    }

    // GET: api/<CommunityController>
    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public IActionResult Get()
    {
        var result = _communityCollectionService.GetCommunityDtos();

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // GET api/<CommunityController>/5
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public IActionResult Get([FromRoute] int id)
    {
        var result = _communityCollectionService.GetCommunityDtoById(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // GET api/<CommunityController>/withusers
    [HttpGet("withusers")]
    [Authorize(Roles = "Administrator")]
    public IActionResult GetWithUsers()
    {
        var result = _communityCollectionService.GetAllCommunityUserDtos();

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // POST api/<CommunityController>/adduser
    [HttpPost("adduser")]
    [Authorize(Roles = "Administrator")]
    public IActionResult PostUserToCommunity([FromBody] CommunityUserDto communityUserDto)
    {
        var user = _userCollectionService.GetById(communityUserDto.UserId);
        var community = _communityCollectionService.GetById(communityUserDto.CommunityId);

        if (user == null || community == null)
        {
            return BadRequest("Could not add user to community.");
        }

        _communityCollectionService.AddUserToCommunity(community, user);

        return Ok();
    }

    // POST api/<CommunityController>/removeuser
    [HttpPost("removeuser")]
    [Authorize(Roles = "Administrator")]
    public IActionResult DeleteUserFromCommunity([FromBody] CommunityUserDto communityUserDto)
    {
        var user = _userCollectionService.GetById(communityUserDto.UserId);
        var community = _communityCollectionService.GetById(communityUserDto.CommunityId);

        if (user == null || community == null)
        {
            return BadRequest("Could not remove user from community.");
        }

        _communityCollectionService.RemoveUserFromCommunity(community, user);

        return Ok();
    }


    // POST api/<CommunityController>
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public IActionResult Post([FromBody] CommunityDto communityDto)
    {
        _communityCollectionService.AddCommunityDto(communityDto);

        return Ok();
    }

    // PUT api/<CommunityController>
    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public IActionResult Put([FromBody] CommunityDto communityDto)
    {
        _communityCollectionService.UpdateCommunityDto(communityDto);

        return Ok();
    }

    // DELETE api/<CommunityController>/5
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public IActionResult Delete([FromRoute] int id)
    {
        _communityCollectionService?.DeleteCommunityDto(id);

        return Ok();
    }
}