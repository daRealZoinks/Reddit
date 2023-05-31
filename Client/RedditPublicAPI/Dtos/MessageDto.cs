namespace RedditPublicAPI.Dtos;

public class MessageDto
{
    public int Id { get; set; }

    public string Content { get; set; }
    public DateTime DateSent { get; set; }

    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
}