using RedditPublicAPI.Enums;

namespace RedditPublicAPI.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime AccountCreationDate { get; set; }
        public string Description { get; set; }
        public Role Role { get; set; }

        public List<MessageDto> SentMessages { get; set; } = new();
        public List<MessageDto> ReceivedMessages { get; set; } = new();
    }
}
