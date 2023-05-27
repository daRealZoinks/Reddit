using System.ComponentModel.DataAnnotations;

namespace Core.Dtos;

public class PostMessageDto
{
    [Required] public string Content { get; set; }

    [Required] public int SenderId { get; set; }
    [Required] public int ReceiverId { get; set; }
}