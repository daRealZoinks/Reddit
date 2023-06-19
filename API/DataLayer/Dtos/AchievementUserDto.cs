using System.ComponentModel.DataAnnotations;

namespace DataLayer.Dtos;

public class AchievementUserDto
{
    [Required] public int AchievementId { get; set; }
    [Required] public int UserId { get; set; }
}
