using System.Net.Http.Headers;
using System.Net.Http.Json;
using RedditPublicAPI.Dtos;
using RedditPublicAPI.Entities;
using RedditPublicAPI.Enums;

namespace RedditPublicAPI;

public static class Users
{
    private const string URI = "https://localhost:5001/api/User";

    public static async Task<List<User>> GetUsers(string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.GetAsync(URI);
            response.EnsureSuccessStatusCode();

            var userDtos = await response.Content.ReadFromJsonAsync<List<UserDto>>() ??
                           throw new Exception("Failed to retrieve user data.");
            var users = userDtos.Select(userDto => new User
            {
                Id = userDto.Id,
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                AccountCreationDate = userDto.AccountCreationDate,
                Description = userDto.Description,
                Role = userDto.Role
            }).ToList();

            return users;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to retrieve user data from {URI}. {ex.Message}", ex);
        }
    }

    public static async Task AddUser(User user, string token)
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

        try
        {
            var response = await httpClient.PostAsJsonAsync(URI, userPayloadDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to add user. {ex.Message}", ex);
        }
    }

    public static async Task<bool> DeleteUser(User user, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.DeleteAsync($"{URI}/{user.Id}");
            response.EnsureSuccessStatusCode();

            return true;
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("404"))
        {
            throw new InvalidOperationException($"User with id {user.Id} not found", ex);
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("401"))
        {
            throw new UnauthorizedAccessException($"Unauthorized operation on user with id {user.Id}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to delete user with Id: {user.Id}. {ex.Message}", ex);
        }
    }

    public static async Task UpdateUser(User user, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
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

            var response = await httpClient.PutAsJsonAsync($"{URI}", userPayloadDto);

            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to update user data. {ex.Message}", ex);
        }
    }

    public static async Task Register(RegisterDto registerDto)
    {
        using HttpClient httpClient = new();

        try
        {
            var response = await httpClient.PostAsJsonAsync($"{URI}/register", registerDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to register user. {ex.Message}", ex);
        }
    }

    public static async Task<string> Login(LoginDto loginDto)
    {
        using HttpClient httpClient = new();

        try
        {
            var response = await httpClient.PostAsJsonAsync($"{URI}/login", loginDto);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("404"))
        {
            throw new InvalidOperationException("User not found.", ex);
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("401"))
        {
            throw new UnauthorizedAccessException("Invalid credentials.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to log in. {ex.Message}", ex);
        }
    }
}