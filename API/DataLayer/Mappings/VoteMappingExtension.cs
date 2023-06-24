using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mappings;

public static class VoteMappingExtension {
	public static List<VoteDto> ToVoteDtos(this List<Vote> votes) {
		if(votes == null)
			return null;

		var results = votes.Select(x => x.ToVoteDto()).ToList();

		return results;
	}

	public static VoteDto ToVoteDto(this Vote vote) {
		if(vote == null) {
			return null;
		}

		var result = new VoteDto {
			Id = vote.Id,
			Upvote = vote.Upvote,
			UserId = vote.UserId,
			PostId = vote.PostId
		};

		return result;
	}
}
