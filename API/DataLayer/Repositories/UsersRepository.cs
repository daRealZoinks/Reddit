using DataLayer.Entities;

namespace DataLayer.Repositories;

public class UsersRepository : RepositoryBase<User>
{
    public UsersRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}