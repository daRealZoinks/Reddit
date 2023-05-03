namespace RedditClient.Models
{
    public class Community
    {
        public string Name { get; set; }
        public User Moderator { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}