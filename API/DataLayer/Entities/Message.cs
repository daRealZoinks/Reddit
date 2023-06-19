namespace DataLayer.Entities;

public class Message : BaseEntity
{
    public string Content { get; set; }
    public DateTimeOffset DateSent { get; set; }

    public int SenderId { get; set; }
    public User Sender { get; set; }

    public int ReceiverId { get; set; }
    public User Receiver { get; set; }
}