using RedditPublicAPI.Dtos;
using RedditPublicAPI.Entities;
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

            List<User> users = new();

            await response.Content.ReadFromJsonAsync<List<User>>().ContinueWith(task =>
            {
                users = task.Result ?? throw new Exception();
            });

            return users;
        }

        public async static void AddUser(User user, string token)
        {
            using HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PostAsJsonAsync(URI, user);

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