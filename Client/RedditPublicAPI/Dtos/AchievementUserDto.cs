using System.ComponentModel.DataAnnotations;

namespace RedditPublicAPI.Dtos;

public class AchievementUserDto
{
    [Required] public int AchievementId { get; set; }
    [Required] public int UserId { get; set; }
}
