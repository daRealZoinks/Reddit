using DataLayer.Enums;

namespace DataLayer.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime AccountCreationDate { get; set; }
    public string Description { get; set; }
    public Role Role { get; set; }
}