using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mappings;

public static class MessageMappingExtention
{
    public static List<MessageDto> ToMessageDtos(this List<Message> messages)
    {
        if (messages == null) return null;

        var results = messages.Select(x => x.ToMessageDto()).ToList();
        return results;
    }

    public static MessageDto ToMessageDto(this Message message)
    {
        if (message == null) return null;

        var result = new MessageDto
        {
            Id = message.Id,
            Content = message.Content,
            DateSent = message.DateSent,
            SenderId = message.SenderId,
            ReceiverId = message.ReceiverId
        };

        return result;
    }
}