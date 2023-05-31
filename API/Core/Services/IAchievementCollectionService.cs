using DataLayer.Dtos;
using DataLayer.Entities;

namespace Core.Services
{
    public interface IAchievementCollectionService : ICollectionService<Achievement>
    {
        List<AchievementDto>? GetAchievementDtos();
        AchievementDto? GetAchievementDtoById(int id);
        void UpdateAchievementDto(AchievementDto achievementDto);
        void AddAchievementDto(AchievementDto achievementDto);
    }
}
