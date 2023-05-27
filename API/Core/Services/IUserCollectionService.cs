using Core.Dtos;
using DataLayer.Dtos;
using DataLayer.Entities;

namespace Core.Services;

public interface IUserCollectionService : ICollectionService<User>
{
    void AddUserDto(UserPayloadDto payload);
    List<UserDto>? GetUserDtos();
    UserDto? GetUserDtoById(int id);
    void UpdateUserDto(UserPayloadDto payload);
    public RegisterDto? Register(RegisterDto payload);
    public string? Login(LoginDto payload);
}