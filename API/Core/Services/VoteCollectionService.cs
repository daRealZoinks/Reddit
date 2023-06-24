using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Mappings;

namespace Core.Services;

public class VoteCollectionService : IVoteCollectionService {
	private readonly UnitOfWork _unitOfWork;

	public VoteCollectionService(UnitOfWork unitOfWork) {
		_unitOfWork = unitOfWork;
	}

	public List<Vote> GetAll() {
		var results = _unitOfWork.VotesRepository.GetAll();

		return results;
	}

	public Vote? GetById(int id) {
		var result = _unitOfWork.VotesRepository.GetById(id);

		return result;
	}

	public void Add(Vote entity) {
		_unitOfWork.VotesRepository.Add(entity);

		_unitOfWork.SaveChanges();
	}

	public void Update(Vote entity) {
		var vote = _unitOfWork.VotesRepository.GetById(entity.Id) ?? throw new Exception("Vote not found");
		vote.Upvote = entity.Upvote;
		vote.UserId = entity.UserId;
		vote.User = entity.User;
		vote.PostId = entity.PostId;
		vote.Post = entity.Post;

		_unitOfWork.VotesRepository.Update(entity);

		_unitOfWork.SaveChanges();
	}

	public void Delete(int id) {
		var vote = _unitOfWork.VotesRepository.GetById(id) ?? throw new Exception("Vote not found");

		_unitOfWork.VotesRepository.Remove(vote);

		_unitOfWork.SaveChanges();
	}

	public VoteDto? GetVoteDtoById(int id) {
		var voteDto = GetById(id)?.ToVoteDto();

		return voteDto;
	}

	public List<VoteDto>? GetVoteDtos() {
		var voteDtos = GetAll().ToVoteDtos();

		return voteDtos;
	}

	public void AddVoteDto(VoteDto voteDto) {
		Vote vote = new() {
			Upvote = voteDto.Upvote,
			UserId = voteDto.UserId,
			PostId = voteDto.PostId
		};

		Add(vote);
	}

	public void UpdateVoteDto(VoteDto voteDto) {
		var vote = GetById(voteDto.Id) ?? throw new Exception("Vote not found");

		vote.Upvote = voteDto.Upvote;
		vote.UserId = voteDto.UserId;
		vote.PostId = voteDto.PostId;

		Update(vote);
	}

	public void DeleteVoteDto(int id) {
		var vote = GetById(id) ?? throw new Exception("Vote not found");

		Delete(vote.Id);
	}
}
