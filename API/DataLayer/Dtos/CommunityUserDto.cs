using System.ComponentModel.DataAnnotations;

namespace DataLayer.Dtos;

public class CommunityUserDto
{
    [Required] public int CommunityId { get; set; }
    [Required] public int UserId { get; set; }
}
