using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public class AchievementRepository : RepositoryBase<Achievement>
    {
        public AchievementRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
