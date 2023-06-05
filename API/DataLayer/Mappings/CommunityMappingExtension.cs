using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mappings;

public static class CommunityMappingExtension
{
    public static List<CommunityDto> ToCommunityDtos(this List<Community> communities)
    {
        if (communities == null)
            return null;

        var results = communities.Select(x => x.ToCommunityDto()).ToList();

        return results;
    }

    public static CommunityDto ToCommunityDto(this Community community)
    {
        if (community == null) return null;

        var result = new CommunityDto
        {
            Id = community.Id,
            Name = community.Name,
            Description = community.Description,
            ModeratorId = community.ModeratorId
        };

        return result;
    }
}