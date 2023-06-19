namespace RedditPublicAPI.Entities;

public class Achievement : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Value { get; set; }

    public List<AchievementUser> AchievementUsers { get; set; } = new();

    public override string ToString()
    {
        return Name;
    }
}