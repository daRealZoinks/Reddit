namespace RedditPublicAPI.Dtos;

public class PostDto
{
    public int Id { get; set; }

    public DateTimeOffset PostDate { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int AuthorId { get; set; }
    public int CommunityId { get; set; }
}