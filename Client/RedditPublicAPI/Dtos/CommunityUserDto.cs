using System.ComponentModel.DataAnnotations;

namespace RedditPublicAPI.Dtos;

public class CommunityUserDto
{
    [Required] public int CommunityId { get; set; }
    [Required] public int UserId { get; set; }
}
