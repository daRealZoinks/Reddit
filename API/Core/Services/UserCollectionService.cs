using Core.Dtos;
using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Mappings;

namespace Core.Services;

public class UserCollectionService : IUserCollectionService
{
    private readonly AuthorizationService _authorizationService;
    private readonly UnitOfWork _unitOfWork;

    public UserCollectionService(UnitOfWork unitOfWork, AuthorizationService authorizationService)
    {
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
    }

    public void Add(User entity)
    {
        _unitOfWork.UsersRepository.Add(entity);
        _unitOfWork.SaveChanges();
    }

    public List<User> GetAll()
    {
        var results = _unitOfWork.UsersRepository.GetAll();
        return results;
    }

    public User? GetById(int id)
    {
        return _unitOfWork.UsersRepository.GetById(id);
    }

    public void Update(User entity)
    {
        var user = _unitOfWork.UsersRepository.GetById(entity.Id) ?? throw new Exception("User not found");

        user.Username = entity.Username;
        user.Email = entity.Email;
        user.PasswordHash = entity.PasswordHash;
        user.AccountCreationDate = entity.AccountCreationDate;
        user.Description = entity.Description;

        _unitOfWork.UsersRepository.Update(user);
        _unitOfWork.SaveChanges();
    }

    public void Delete(int id)
    {
        var user = _unitOfWork.UsersRepository.GetById(id) ?? throw new Exception("User not found");

        _unitOfWork.UsersRepository.Remove(user);
        _unitOfWork.SaveChanges();
    }

    public UserDto? GetUserDtoById(int id)
    {
        var userDto = GetById(id)?.ToUserDto();
        return userDto;
    }

    public List<UserDto>? GetUserDtos()
    {
        var userDtos = GetAll().ToUserDtos();
        return userDtos;
    }

    public void AddUserDto(UserPayloadDto payload)
    {
        User user = new()
        {
            Username = payload.Username,
            Email = payload.Email,
            PasswordHash = payload.PasswordHash,
            AccountCreationDate = payload.AccountCreationDate,
            Description = payload.Description,
            Role = payload.Role
        };

        Add(user);
    }

    public void UpdateUserDto(UserPayloadDto payload)
    {
        var user = GetById(payload.Id) ?? throw new Exception("User not found");

        user.Username = payload.Username;
        user.Email = payload.Email;
        user.PasswordHash = payload.PasswordHash;
        user.AccountCreationDate = payload.AccountCreationDate;
        user.Description = payload.Description;
        user.Role = payload.Role;

        Update(user);
    }

    public RegisterDto? Register(RegisterDto payload)
    {
        if (payload == null) return null;

        var hashedPassword = _authorizationService.HashPassword(payload.Password);

        if (hashedPassword == null) return null;

        User user = new()
        {
            Username = string.Empty,
            Email = payload.Email,
            PasswordHash = hashedPassword,
            AccountCreationDate = DateTime.Now,
            Description = string.Empty,
            Role = payload.Role
        };

        Add(user);

        return payload;
    }

    public string? Login(LoginDto payload)
    {
        var user = _unitOfWork.UsersRepository.GetByEmail(payload.Email);

        if (user == null) return null;

        return !_authorizationService.VerifyHashedPassword(user.PasswordHash, payload.Password) ? null : _authorizationService.GetToken(user);
    }
}