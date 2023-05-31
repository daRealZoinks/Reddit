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

    public AchievementController(IAchievementCollectionService achievementCollectionService)
    {
        _achievementCollectionService = achievementCollectionService ??
                                        throw new ArgumentNullException(nameof(achievementCollectionService));
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