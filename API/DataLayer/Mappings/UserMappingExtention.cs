using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mappings;

public static class UserMappingExtention
{
    public static List<UserDto> ToUserDtos(this List<User> users)
    {
        if (users == null)
            return null;

        var results = users.Select(x => x.ToUserDto()).ToList();
        return results;
    }

    public static UserDto ToUserDto(this User user)
    {
        if (user == null)
            return null;

        var result = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            AccountCreationDate = user.AccountCreationDate,
            Description = user.Description,
            Role = user.Role
        };

        return result;
    }
}