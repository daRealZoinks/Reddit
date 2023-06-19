using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Mappings;

namespace Core.Services;

public class AchievementCollectionService : IAchievementCollectionService
{
    private readonly UnitOfWork _unitOfWork;

    public AchievementCollectionService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Add(Achievement entity)
    {
        _unitOfWork.AchievementRepository.Add(entity);

        _unitOfWork.SaveChanges();
    }

    public void Delete(int id)
    {
        var achievement = _unitOfWork.AchievementRepository.GetById(id) ?? throw new Exception("Achievement not found");

        _unitOfWork.AchievementRepository.Remove(achievement);

        _unitOfWork.SaveChanges();
    }

    public List<Achievement> GetAll()
    {
        var results = _unitOfWork.AchievementRepository.GetAll();
        return results;
    }

    public Achievement? GetById(int id)
    {
        return _unitOfWork.AchievementRepository.GetById(id);
    }

    public void Update(Achievement entity)
    {
        var achievement = _unitOfWork.AchievementRepository.GetById(entity.Id) ??
                          throw new Exception("Achievement not found");

        achievement.Name = entity.Name;
        achievement.Description = entity.Description;
        achievement.Value = entity.Value;

        _unitOfWork.AchievementRepository.Update(entity);

        _unitOfWork.SaveChanges();
    }

    public void AddAchievementDto(AchievementDto achievementDto)
    {
        var achievement = new Achievement
        {
            Name = achievementDto.Name,
            Description = achievementDto.Description,
            Value = achievementDto.Value
        };

        Add(achievement);
    }

    public List<AchievementDto>? GetAchievementDtos()
    {
        var achievementDtos = GetAll().ToAchievementDtos();

        return achievementDtos;
    }

    public List<AchievementUserDto>? GetAllAchievementUserDtos()
    {
        var achievementDtos = _unitOfWork.AchievementUserRepository.GetAll().ToAchievementUserDtos();

        return achievementDtos;
    }

    public AchievementDto? GetAchievementDtoById(int id)
    {
        var achievementDtos = GetById(id)?.ToAchievementDto();

        return achievementDtos;
    }

    public void UpdateAchievementDto(AchievementDto achievementDto)
    {
        var achievement = GetById(achievementDto.Id) ?? throw new Exception("Achievement not found");

        achievement.Name = achievementDto.Name;
        achievement.Description = achievementDto.Description;
        achievement.Value = achievementDto.Value;

        Update(achievement);
    }

    public void AddAchievementToUser(Achievement achievement, User user)
    {
        var achievementUser = new AchievementUser
        {
            AchievementId = achievement.Id,
            Achievement = achievement,
            UserId = user.Id,
            User = user
        };

        _unitOfWork.AchievementUserRepository.Add(achievementUser);

        achievement.AchievementUsers.Add(achievementUser);
        user.AchievementUsers.Add(achievementUser);

        _unitOfWork.SaveChanges();
    }

    public void RemoveAchievementFromUser(Achievement achievement, User user)
    {
        var achievementUser = _unitOfWork.AchievementUserRepository.GetAll()
            .FirstOrDefault(x => x.AchievementId == achievement.Id && x.UserId == user.Id);

        if (achievementUser == null) return;

        _unitOfWork.AchievementUserRepository.Remove(achievementUser);

        achievement.AchievementUsers.Remove(achievementUser);
        user.AchievementUsers.Remove(achievementUser);

        _unitOfWork.SaveChanges();
    }
}