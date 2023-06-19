using RedditPublicAPI.Enums;

namespace RedditPublicAPI.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTimeOffset AccountCreationDate { get; set; }
    public string Description { get; set; }
    public Role Role { get; set; }

    public int ModeratedCommunityId { get; set; }
    public Community? ModeratedCommunity { get; set; }

    public List<Message> SentMessages { get; set; } = new();
    public List<Message> ReceivedMessages { get; set; } = new();
    public List<AchievementUser> AchievementUsers { get; set; } = new();
    public List<Post> Posts { get; set; } = new();
    public List<CommunityUser> CommunityUsers { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();

    public override string ToString()
    {
        return Username;
    }
}