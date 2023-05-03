using System;

namespace RedditClient.Models
{
    public class Message
    {
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
    }
}