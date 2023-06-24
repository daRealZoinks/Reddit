namespace RedditPublicAPI.Entities;

public class Vote : BaseEntity {
	public bool Upvote {
		get; set;
	}

	public int UserId {
		get; set;
	}

	public User User {
		get; set;
	}

	public int PostId {
		get; set;
	}

	public Post Post {
		get; set;
	}

	public override string ToString() {
		return Upvote.ToString();
	}
	// TODO:?
}
