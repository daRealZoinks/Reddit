using RedditPublicAPI.Dtos;
using RedditPublicAPI.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RedditPublicAPI;

public class Messages
{
    private const string URI = "https://localhost:5001/api/Message";

    public static async Task<List<Message>> GetMessages(string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.GetAsync(URI);
            response.EnsureSuccessStatusCode();

            var messageDtos = await response.Content.ReadFromJsonAsync<List<MessageDto>>();
            if (messageDtos == null || messageDtos.Count == 0) throw new Exception("No messages found.");

            return messageDtos.Select(messageDto => new Message
            {
                Id = messageDto.Id,
                Content = messageDto.Content,
                DateSent = messageDto.DateSent,
                SenderId = messageDto.SenderId,
                ReceiverId = messageDto.ReceiverId
            }).ToList();
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Failed to retrieve message data. {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to retrieve message data. {ex.Message}", ex);
        }
    }

    public static async Task AddMessage(Message message, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.PostAsJsonAsync(URI, message);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to add message. {ex.Message}", ex);
        }
    }

    public static async void DeleteMessage(Message message, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.DeleteAsync($"{URI}/{message.Id}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("404"))
        {
            throw new InvalidOperationException($"Message with id {message.Id} not found", ex);
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("401"))
        {
            throw new UnauthorizedAccessException($"Unauthorized operation on message with id {message.Id}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to delete message with Id: {message.Id}. {ex.Message}", ex);
        }
    }

    public static async void UpdateMessage(Message message, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.PutAsJsonAsync(URI, message);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception("Failed to update message.");
        }
    }
}