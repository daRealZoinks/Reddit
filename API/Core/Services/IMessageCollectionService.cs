using Core.Dtos;
using DataLayer.Dtos;
using DataLayer.Entities;

namespace Core.Services;

public interface IMessageCollectionService : ICollectionService<Message>
{
    void AddMessageDto(PostMessageDto postMessageDto);
    List<MessageDto>? GetMessageDtos();
    MessageDto? GetMessageDtoById(int id);
    void UpdateMessageDto(MessageDto messageDto);
}