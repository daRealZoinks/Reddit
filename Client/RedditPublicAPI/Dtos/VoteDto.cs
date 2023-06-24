namespace RedditPublicAPI.Dtos;

public class VoteDto {
	public int Id {
		get; set;
	}
	public bool Upvote {
		get; set;
	}
	public int UserId {
		get; set;
	}
	public int PostId {
		get; set;
	}
}
