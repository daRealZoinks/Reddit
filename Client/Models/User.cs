namespace RedditClient.Models {
	public class User {
		public string Username {
			get; set;
		}
		public string Email {
			get; set;
		}
		public string Password {
			get; set;
		}
		// public DateOnly AccountCreationDate { get; set; }
		public string Description {
			get; set;
		}
	}
}