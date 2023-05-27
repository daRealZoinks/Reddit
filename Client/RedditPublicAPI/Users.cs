using RedditPublicAPI.Dtos;
using RedditPublicAPI.Entities;
using RedditPublicAPI.Enums;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RedditPublicAPI
{
    public class Users
    {
        private const string URI = "https://localhost:7214/api/User";

        public async static Task<List<User>> GetUsers(string token)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.GetAsync(URI);
            response.EnsureSuccessStatusCode();

            var userDtos = await response.Content.ReadFromJsonAsync<List<UserDto>>() ?? throw new Exception("Failed to retrieve user data.");
            List<User> users = userDtos.Select(userDto => new User
            {
                Id = userDto.Id,
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                AccountCreationDate = userDto.AccountCreationDate,
                Description = userDto.Description,
                Role = userDto.Role,
                SentMessages = userDto.SentMessages.Select(messageDto => new Message
                {
                    Id = messageDto.Id,
                    Content = messageDto.Content,
                    DateSent = messageDto.DateSent,
                    SenderId = messageDto.SenderId,
                    ReceiverId = messageDto.ReceiverId
                }).ToList(),
                ReceivedMessages = userDto.ReceivedMessages.Select(messageDto => new Message
                {
                    Id = messageDto.Id,
                    Content = messageDto.Content,
                    DateSent = messageDto.DateSent,
                    SenderId = messageDto.SenderId,
                    ReceiverId = messageDto.ReceiverId
                }).ToList()
            }).ToList();

            return users;
        }

        public async static void AddUser(User user, string token)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            UserPayloadDto userPayloadDto = new()
            {
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                AccountCreationDate = user.AccountCreationDate,
                Description = user.Description,
                Role = Role.User
            };

            var response = await httpClient.PostAsJsonAsync(URI, userPayloadDto);

            response.EnsureSuccessStatusCode();
        }

        public async static void DeleteUser(User user, string token)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.DeleteAsync($"{URI}/{user.Id}");

            response.EnsureSuccessStatusCode();
        }

        public async static void UpdateUser(User user, string token)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            UserPayloadDto userPayloadDto = new()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                AccountCreationDate = user.AccountCreationDate,
                Description = user.Description,
                Role = Role.User
            };

            var response = await httpClient.PutAsJsonAsync(URI, user);

            response.EnsureSuccessStatusCode();
        }

        public async static void Register(RegisterDto registerDto)
        {
            using HttpClient httpClient = new();
            var response = await httpClient.PostAsJsonAsync($"{URI}/register", registerDto);

            response.EnsureSuccessStatusCode();
        }

        public async static Task<string> Login(LoginDto loginDto)
        {
            using HttpClient httpClient = new();
            var response = await httpClient.PostAsJsonAsync($"{URI}/login", loginDto);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}