using DataLayer.Entities;

namespace Core.Services
{
    public class UserCollectionService : IUserCollectionService
    {
        static List<User> _users = new()
        {
            new User()
            {
                Id = Guid.NewGuid(),
                Username = "test1",
                Email = "celmaitare@yoohoo.com",
                Password = "parolagrea",
                AccountCreationDate = DateTime.Today,
                Description = "1",
            },
            new User()
            {
                Id = Guid.NewGuid(),
                Username = "test2",
                Email = "celmaiputintare@googoo.com",
                Password = "parolasimpla",
                AccountCreationDate = DateTime.Today.AddDays(-1),
                Description = "2",
            },
            new User()
            {
                Id = Guid.NewGuid(),
                Username = "test3",
                Email = "altreileamail@beeng.com",
                Password = "parolasimaisimpla",
                AccountCreationDate = DateTime.Today.AddDays(-2),
                Description = "3",
            },
        };

        public User? Get(Guid id)
        {
            return _users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetAll()
        {
            return _users;
        }

        public void Create(User model)
        {
            _users.Add(model);
        }

        public void Delete(Guid id)
        {
            _users.Remove(_users.FirstOrDefault(x => x.Id == id));
        }

        public void Update(User model)
        {
            var user = _users.FirstOrDefault(x => x.Id == model.Id);

            if (user != null)
            {
                user.Username = model.Username;
                user.Email = model.Email;
                user.Password = model.Password;
                user.Description = model.Description;
            }
        }
    }
}
