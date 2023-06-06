namespace DataLayer.Dtos;

public class CommunityDto
{
    public int Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public int ModeratorId { get; set; }
}