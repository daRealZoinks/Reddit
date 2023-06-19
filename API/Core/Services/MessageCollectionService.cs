using Core.Dtos;
using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Mappings;

namespace Core.Services;

public class MessageCollectionService : IMessageCollectionService
{
    private readonly UnitOfWork _unitOfWork;

    public MessageCollectionService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void Add(Message entity)
    {
        _unitOfWork.MessagesRepository.Add(entity);
        _unitOfWork.SaveChanges();
    }

    public void Delete(int id)
    {
        var message = _unitOfWork.MessagesRepository.GetById(id) ?? throw new Exception("Message not found");

        _unitOfWork.MessagesRepository.Remove(message);
        _unitOfWork.SaveChanges();
    }

    public List<Message> GetAll()
    {
        var results = _unitOfWork.MessagesRepository.GetAll();
        return results;
    }

    public Message? GetById(int id)
    {
        return _unitOfWork.MessagesRepository.GetById(id);
    }

    public void Update(Message entity)
    {
        var message = _unitOfWork.MessagesRepository.GetById(entity.Id) ?? throw new Exception("Message not found");

        message.Content = entity.Content;
        message.SenderId = entity.SenderId;
        message.ReceiverId = entity.ReceiverId;
        message.DateSent = entity.DateSent;

        _unitOfWork.MessagesRepository.Update(message);
        _unitOfWork.SaveChanges();
    }

    public void AddMessageDto(PostMessageDto postMessageDto)
    {
        var message = new Message
        {
            Content = postMessageDto.Content,
            DateSent = DateTimeOffset.Now.UtcDateTime,
            SenderId = postMessageDto.SenderId,
            ReceiverId = postMessageDto.ReceiverId
        };

        Add(message);
    }

    public List<MessageDto> GetMessageDtos()
    {
        var messageDtos = GetAll().ToMessageDtos();
        return messageDtos;
    }

    public MessageDto? GetMessageDtoById(int id)
    {
        var messageDto = GetById(id)?.ToMessageDto();
        return messageDto;
    }

    public void UpdateMessageDto(MessageDto messageDto)
    {
        var message = GetById(messageDto.Id) ?? throw new Exception("User not found");

        message.Content = messageDto.Content;
        message.DateSent = messageDto.DateSent;
        message.SenderId = messageDto.SenderId;
        message.ReceiverId = messageDto.ReceiverId;

        Update(message);
    }
}