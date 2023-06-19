namespace RedditPublicAPI.Entities;

public class Comment : BaseEntity
{
    public DateTimeOffset PostDate { get; set; }
    public string Content { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; }

    public override string ToString()
    {
        return Content;
    }
}
