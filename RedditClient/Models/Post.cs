namespace RedditClient.Models;

public class Post
{
    public User Author { get; set; }

    // public DateOnly Date { get; set; }
    public string Title { get; set; }
}