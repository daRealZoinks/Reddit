using DataLayer.Dtos;
using DataLayer.Entities;

namespace Core.Services;

public interface IVoteCollectionService : ICollectionService<Vote> {
	List<VoteDto>? GetVoteDtos();
	VoteDto? GetVoteDtoById(int id);
	void AddVoteDto(VoteDto voteDto);
	void UpdateVoteDto(VoteDto voteDto);
	void DeleteVoteDto(int id);
}
