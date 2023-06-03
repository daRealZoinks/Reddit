using DataLayer.Entities;

namespace DataLayer.Repositories;

public class CommentsRepository : RepositoryBase<Comment> {
	public CommentsRepository(AppDbContext appDbContext) : base(appDbContext) {
	}
}
