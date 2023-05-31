using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mappings;

public static class AchievementMappingExtension
{
    public static List<AchievementDto> ToAchievementDtos(this List<Achievement> achievements)
    {
        if (achievements == null) return null;

        var results = achievements.Select(x => x.ToAchievementDto()).ToList();
        return results;
    }

    public static AchievementDto ToAchievementDto(this Achievement achievement)
    {
        if (achievement == null) return null;

        var result = new AchievementDto
        {
            Id = achievement.Id,
            Name = achievement.Name,
            Description = achievement.Description,
            Value = achievement.Value
        };

        return result;
    }
}