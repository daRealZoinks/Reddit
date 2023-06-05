using System.ComponentModel.DataAnnotations;
using DataLayer.Enums;

namespace Core.Dtos;

public class RegisterDto
{
    [Required] public string Password { get; set; }
    [Required] public string Email { get; set; }
    [Required] public Role Role { get; set; }
}