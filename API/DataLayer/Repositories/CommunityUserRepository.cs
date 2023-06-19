using DataLayer.Entities;

namespace DataLayer.Repositories;

public class CommunityUserRepository : RepositoryBase<CommunityUser>
{
    public CommunityUserRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}