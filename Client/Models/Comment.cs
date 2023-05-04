namespace RedditClient.Models {
	public class Comment {
		public Post Post {
			get; set;
		}
		public User User {
			get; set;
		}
		// public DateOnly Date { get; set; }
		public string Content {
			get; set;
		}
	}
}