using System.ComponentModel.DataAnnotations;

namespace RedditPublicAPI.Dtos;

public class CommunityPayloadDto
{
    [Required] public int Id { get; set; }

    [Required] public string Name { get; set; }

    [Required] public string Description { get; set; }

    [Required] public int ModeratorId { get; set; }
}