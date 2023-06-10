using DataLayer.Dtos;
using DataLayer.Entities;

namespace Core.Services;

public interface ICommunityCollectionService : ICollectionService<Community>
{
    List<CommunityDto>? GetCommunityDtos();
    CommunityDto? GetCommunityDtoById(int id);
    void AddCommunityDto(CommunityDto communityDto);
    void UpdateCommunityDto(CommunityDto communityDto);
    void DeleteCommunityDto(int id);
    void AddUserToCommunity(Community community, User user);
    void RemoveUserFromCommunity(Community community, User user);
}