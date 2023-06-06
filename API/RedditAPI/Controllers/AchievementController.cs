using Core.Dtos;
using Core.Services;
using DataLayer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedditAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AchievementController : ControllerBase
{
    private readonly IAchievementCollectionService _achievementCollectionService;
    private readonly IUserCollectionService _userCollectionService;

    public AchievementController(IAchievementCollectionService achievementCollectionService, IUserCollectionService userCollectionService)
    {
        _achievementCollectionService = achievementCollectionService ?? throw new ArgumentNullException(nameof(achievementCollectionService));
        _userCollectionService = userCollectionService ?? throw new ArgumentNullException(nameof(userCollectionService));
    }


    // GET: api/<AchievementController>
    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public IActionResult Get()
    {
        var result = _achievementCollectionService.GetAchievementDtos();

        if (result == null) return NotFound();

        return Ok(result);
    }

    // GET api/<AchievementController>/5
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public IActionResult Get([FromRoute] int id)
    {
        var result = _achievementCollectionService.GetAchievementDtoById(id);

        if (result == null) return NotFound();

        return Ok(result);
    }

    // POST api/<AchievementController>/addachievementtouser
    [HttpPost("addachievementtouser")]
    [Authorize(Roles = "Administrator")]
    public IActionResult PostAchievementToUser(AchievementToUserDto userToCommunityDto)
    {
        var user = _userCollectionService.GetById(userToCommunityDto.UserId);
        var achievement = _achievementCollectionService.GetById(userToCommunityDto.AchievementId);

        if (user == null || achievement == null)
        {
            return BadRequest("Could not add user to community.");
        }

        _achievementCollectionService.AddAchievementToUser(achievement, user);

        return Ok();
    }

    // POST api/<AchievementController>/removeachievementfromuser
    [HttpPost("removeachievementfromuser")]
    [Authorize(Roles = "Administrator")]
    public IActionResult DeleteAchievementFromUser(AchievementToUserDto userToCommunityDto)
    {
        var user = _userCollectionService.GetById(userToCommunityDto.UserId);
        var achievement = _achievementCollectionService.GetById(userToCommunityDto.AchievementId);

        if (user == null || achievement == null)
        {
            return BadRequest("Could not remove user from community.");
        }

        _achievementCollectionService.RemoveAchievementFromUser(achievement, user);

        return Ok();
    }

    // POST api/<AchievementController>
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public IActionResult Post([FromBody] AchievementDto achievementPayloadDto)
    {
        _achievementCollectionService.AddAchievementDto(achievementPayloadDto);

        return Ok();
    }

    // PUT api/<AchievementController>
    [HttpPut]
    [Authorize(Roles = "Administrator")]
    public IActionResult Put([FromBody] AchievementDto achievementDto)
    {
        _achievementCollectionService.UpdateAchievementDto(achievementDto);

        return Ok();
    }

    // DELETE api/<AchievementController>/5
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Administrator")]
    public IActionResult Delete([FromRoute] int id)
    {
        _achievementCollectionService.Delete(id);

        return Ok();
    }
}