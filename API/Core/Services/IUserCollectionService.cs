using Core.Dtos;
using DataLayer.Entities;

namespace Core.Services;

public interface IUserCollectionService : ICollectionService<User>
{
    public RegisterDto? Register(RegisterDto payload);
    public string? Login(LoginDto payload);
}