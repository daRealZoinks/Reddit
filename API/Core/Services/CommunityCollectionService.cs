using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Mappings;

namespace Core.Services;

public class CommunityCollectionService : ICommunityCollectionService
{
    private readonly UnitOfWork _unitOfWork;

    public CommunityCollectionService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<Community> GetAll()
    {
        var results = _unitOfWork.CommunityRepository.GetAll();

        return results;
    }

    public Community? GetById(int id)
    {
        return _unitOfWork.CommunityRepository.GetById(id);
    }

    public void Add(Community entity)
    {
        _unitOfWork.CommunityRepository.Add(entity);

        var user = _unitOfWork.UsersRepository.GetById(entity.ModeratorId) ?? throw new Exception("User not found");

        user.ModeratedCommunity = entity;

        _unitOfWork.SaveChanges();
    }

    public void Delete(int id)
    {
        var community = _unitOfWork.CommunityRepository.GetById(id) ?? throw new Exception("Community not found");
        _unitOfWork.CommunityRepository.Remove(community);

        _unitOfWork.SaveChanges();
    }

    public void Update(Community entity)
    {
        var community = _unitOfWork.CommunityRepository.GetById(entity.Id) ??
                        throw new Exception("Community not found");
        community.Name = entity.Name;
        community.Description = entity.Description;
        community.ModeratorId = entity.ModeratorId;
        community.Moderator = entity.Moderator;

        _unitOfWork.CommunityRepository.Update(entity);

        _unitOfWork.SaveChanges();
    }

    public CommunityDto? GetCommunityDtoById(int id)
    {
        var communityDto = GetById(id)?.ToCommunityDto();

        return communityDto;
    }

    public List<CommunityDto>? GetCommunityDtos()
    {
        var communityDtos = GetAll().ToCommunityDtos();

        return communityDtos;
    }

    public List<CommunityUserDto>? GetAllCommunityUserDtos()
    {
        var communityUserDtos = _unitOfWork.CommunityUserRepository.GetAll().ToCommunityUserDtos();

        return communityUserDtos;
    }

    public void AddCommunityDto(CommunityDto communityDto)
    {
        Community community = new()
        {
            Name = communityDto.Name,
            Description = communityDto.Description,
            ModeratorId = communityDto.ModeratorId
        };

        Add(community);
    }

    public void UpdateCommunityDto(CommunityDto communityDto)
    {
        var community = GetById(communityDto.Id) ?? throw new Exception("Community not found");

        community.Name = communityDto.Name;
        community.Description = communityDto.Description;
        community.ModeratorId = communityDto.ModeratorId;

        Update(community);
    }

    public void DeleteCommunityDto(int id)
    {
        var community = GetById(id) ?? throw new Exception("Community not found");

        Delete(community.Id);
    }

    public void AddUserToCommunity(Community community, User user)
    {
        var communityUser = new CommunityUser
        {
            CommunityId = community.Id,
            Community = community,
            UserId = user.Id,
            User = user
        };

        _unitOfWork.CommunityUserRepository.Add(communityUser);

        community.CommunityUsers.Add(communityUser);
        user.CommunityUsers.Add(communityUser);

        _unitOfWork.SaveChanges();
    }

    public void RemoveUserFromCommunity(Community community, User user)
    {
        var communityUser = _unitOfWork.CommunityUserRepository.GetAll()
            .FirstOrDefault(x => x.CommunityId == community.Id && x.UserId == user.Id);

        if (communityUser == null) return;

        _unitOfWork.CommunityUserRepository.Remove(communityUser);

        community.CommunityUsers.Remove(communityUser);
        user.CommunityUsers.Remove(communityUser);

        _unitOfWork.SaveChanges();
    }
}