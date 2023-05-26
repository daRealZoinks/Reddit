using RedditPublicAPI.Entities;
using System.Net.Http.Json;

namespace RedditPublicAPI
{
    public class Users
    {
        private const string URI = "https://localhost:7214/api/User";

        public async static Task<List<User>> GetUsers()
        {
            using HttpClient httpClient = new();
            var response = await httpClient.GetAsync(URI);

            response.EnsureSuccessStatusCode();

            List<User> users = new();

            await response.Content.ReadFromJsonAsync<List<User>>().ContinueWith(task =>
            {
                users = task.Result ?? throw new Exception();
            });

            return users;
        }

        public async static void AddUser(User user)
        {
            using HttpClient httpClient = new();
            var response = await httpClient.PostAsJsonAsync(URI, user);

            response.EnsureSuccessStatusCode();
        }

        public async static void DeleteUser(User user)
        {
            using HttpClient httpClient = new();
            var response = await httpClient.DeleteAsync($"{URI}/{user.Id}");

            response.EnsureSuccessStatusCode();
        }

        public async static void UpdateUser(User user)
        {
            using HttpClient httpClient = new();
            var response = await httpClient.PutAsJsonAsync("https://localhost:7214/api/User", user);
        }
    }
}