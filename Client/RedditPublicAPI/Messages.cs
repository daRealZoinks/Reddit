using RedditPublicAPI.Dtos;
using RedditPublicAPI.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RedditPublicAPI
{
    public class Messages
    {
        private const string URI = "https://localhost:7214/api/Message";

        public async static Task<List<Message>> GetMessages(string token)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(URI);
            response.EnsureSuccessStatusCode();

            var messageDtos = await response.Content.ReadFromJsonAsync<List<MessageDto>>() ?? throw new Exception("Failed to retrieve message data.");
            List<Message> messages = messageDtos.Select(messageDto => new Message
            {
                Id = messageDto.Id,
                Content = messageDto.Content,
                DateSent = messageDto.DateSent,
                SenderId = messageDto.SenderId,
                ReceiverId = messageDto.ReceiverId
            }).ToList();

            return messages;
        }

        public async static void AddMessage(Message message, string token)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PostAsJsonAsync(URI, message);

            response.EnsureSuccessStatusCode();
        }

        public async static void DeleteMessage(Message message, string token)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.DeleteAsync($"{URI}/{message.Id}");

            response.EnsureSuccessStatusCode();
        }

        public async static void UpdateMessage(Message message, string token)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PutAsJsonAsync(URI, message);

            response.EnsureSuccessStatusCode();
        }
    }
}
