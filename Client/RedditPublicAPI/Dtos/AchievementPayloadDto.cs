using System.ComponentModel.DataAnnotations;

namespace RedditPublicAPI.Dtos;

public class AchievementPayloadDto
{
    [Required] public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }

    [Required] public int Value { get; set; }
}