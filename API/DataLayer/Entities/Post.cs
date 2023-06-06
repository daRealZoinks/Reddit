namespace DataLayer.Entities;

public class Post : BaseEntity
{
    public DateTime PostDate { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; }

    public int CommunityId { get; set; }
    public Community Community { get; set; }
}