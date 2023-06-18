namespace DataLayer.Entities;

public class Comment : BaseEntity
{
    public DateTime PostDate { get; set; }
    public string Content { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; }
}
