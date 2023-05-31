using RedditPublicAPI.Dtos;
using RedditPublicAPI.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RedditPublicAPI;

public class Achievements
{
    private const string URI = "https://localhost:5001/api/Achievement";

    public static async Task<List<Achievement>> GetAchievements(string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.GetAsync(URI);
            response.EnsureSuccessStatusCode();

            var achievementDtos = await response.Content.ReadFromJsonAsync<List<AchievementDto>>() ??
                                  throw new Exception("Failed to retrieve achievement data.");
            var achievements = achievementDtos.Select(achievementDto => new Achievement
            {
                Id = achievementDto.Id,
                Name = achievementDto.Name,
                Description = achievementDto.Description,
                Value = achievementDto.Value
            }).ToList();

            return achievements;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to retrieve achievement data from {URI}. {ex.Message}", ex);
        }
    }

    public static async Task AddAchievement(Achievement achievement, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        AchievementPayloadDto achievementPayloadDto = new()
        {
            Name = achievement.Name,
            Description = achievement.Description,
            Value = achievement.Value
        };

        try
        {
            var response = await httpClient.PostAsJsonAsync(URI, achievementPayloadDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to add achievement data to {URI}. {ex.Message}", ex);
        }
    }

    public static async Task DeleteAchievement(Achievement achievement, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.DeleteAsync($"{URI}/{achievement.Id}");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to delete achievement data from {URI}. {ex.Message}", ex);
        }
    }

    public static async Task UpdateAchievement(Achievement achievement, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        AchievementDto achievementPayloadDto = new()
        {
            Id = achievement.Id,
            Name = achievement.Name,
            Description = achievement.Description,
            Value = achievement.Value
        };

        try
        {
            var response = await httpClient.PutAsJsonAsync($"{URI}", achievementPayloadDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to update achievement data in {URI}. {ex.Message}", ex);
        }
    }
}