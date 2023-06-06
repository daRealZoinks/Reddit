namespace DataLayer.Dtos;

public class PostDto
{
    public int Id { get; set; }

    public DateTime PostDate { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int AuthorId { get; set; }
    public int CommunityId { get; set; }
}