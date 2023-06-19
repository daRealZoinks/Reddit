using DataLayer.Entities;

namespace DataLayer.Repositories;

public class AchievementUserRepository : RepositoryBase<AchievementUser>
{
    public AchievementUserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}