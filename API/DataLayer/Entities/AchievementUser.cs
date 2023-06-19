namespace DataLayer.Entities;

public class AchievementUser : BaseEntity
{
    public int AchievementId { get; set; }
    public Achievement Achievement { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
