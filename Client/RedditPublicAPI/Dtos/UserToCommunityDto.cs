using System.ComponentModel.DataAnnotations;

namespace RedditPublicAPI.Dtos
{
    public class UserToCommunityDto
    {
        [Required] public int UserId { get; set; }
        [Required] public int CommunityId { get; set; }
    }
}
