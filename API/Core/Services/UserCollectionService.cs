using DataLayer;
using DataLayer.Entities;

namespace Core.Services;

public class UserCollectionService : IUserCollectionService
{
    private readonly UnitOfWork _unitOfWork;

    public UserCollectionService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
        user.Password = entity.Password;
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
}