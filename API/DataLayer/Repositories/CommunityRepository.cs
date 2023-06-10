using DataLayer.Entities;

namespace DataLayer.Repositories;

public class CommunityRepository : RepositoryBase<Community>
{
    public CommunityRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}