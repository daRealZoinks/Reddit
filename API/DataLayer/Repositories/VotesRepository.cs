using DataLayer.Entities;

namespace DataLayer.Repositories;

public class VotesRepository : RepositoryBase<Vote> {
	public VotesRepository(AppDbContext appDbContext) : base(appDbContext) {
	}
}
