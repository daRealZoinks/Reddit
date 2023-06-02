using DataLayer.Entities;

namespace DataLayer.Repositories;

public class PostsRepository : RepositoryBase<Post> {
	public PostsRepository(AppDbContext appDbContext) : base(appDbContext) {

	}
}