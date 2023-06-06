using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class AchievementToUserDto
    {
        [Required] public int UserId { get; set; }
        [Required] public int AchievementId { get; set; }
    }
}
