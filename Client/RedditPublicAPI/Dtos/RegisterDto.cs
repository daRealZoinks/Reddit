using RedditPublicAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace RedditPublicAPI.Dtos;

public class RegisterDto
{
    [Required] public string Password { get; set; }
    [Required] public string Email { get; set; }
    [Required] public Role Role { get; set; }
}