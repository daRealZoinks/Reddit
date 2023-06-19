using DataLayer.Dtos;
using DataLayer.Entities;

namespace Core.Services;

public interface IAchievementCollectionService : ICollectionService<Achievement>
{
    List<AchievementDto>? GetAchievementDtos();
    List<AchievementUserDto>? GetAllAchievementUserDtos();
    AchievementDto? GetAchievementDtoById(int id);
    void UpdateAchievementDto(AchievementDto achievementDto);
    void AddAchievementDto(AchievementDto achievementDto);
    void AddAchievementToUser(Achievement achievement, User user);
    void RemoveAchievementFromUser(Achievement achievement, User user);
}