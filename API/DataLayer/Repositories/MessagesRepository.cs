using DataLayer.Entities;

namespace DataLayer.Repositories
{
    public class MessagesRepository : RepositoryBase<Message>
    {
        public MessagesRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
