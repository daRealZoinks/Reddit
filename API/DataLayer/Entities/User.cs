using DataLayer.Enums;

namespace DataLayer.Entities;

public class User : BaseEntity {
	public string Username {
		get; set;
	}
	public string Email {
		get; set;
	}
	public string PasswordHash {
		get; set;
	}
	public DateTime AccountCreationDate {
		get; set;
	}
	public string Description {
		get; set;
	}
	public Role Role {
		get; set;
	}
	public int ModeratedCommunityId {
		get; set;
	}
	public Community? ModeratedCommunity {
		get; set;
	}

	public List<Message> SentMessages { get; set; } = new();
	public List<Message> ReceivedMessages { get; set; } = new();
	public List<Achievement> Achievements { get; set; } = new();
	public List<Comment> Comments { get; set; } = new();
	// public List<Community> Communities { get; set; } = new();
}