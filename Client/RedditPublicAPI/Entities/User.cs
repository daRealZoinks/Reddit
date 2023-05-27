using RedditPublicAPI.Enums;

namespace RedditPublicAPI.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime AccountCreationDate { get; set; }
    public string Description { get; set; }
    public Role Role { get; set; }

    public List<Message> SentMessages { get; set; } = new();
    public List<Message> ReceivedMessages { get; set; } = new();

    public override string ToString()
    {
        return Username;
    }
}