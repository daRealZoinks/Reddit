﻿using RedditPublicAPI.Dtos;
using RedditPublicAPI.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RedditPublicAPI;

public static class Achievements
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

    public static async Task<List<AchievementUser>> GetWithUsers(string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.GetAsync($"{URI}/withusers");
            response.EnsureSuccessStatusCode();

            var achievementUserDtos = await response.Content.ReadFromJsonAsync<List<AchievementUserDto>>() ??
                                  throw new Exception("Failed to retrieve achievement data.");
            var achievementUsers = achievementUserDtos.Select(achievementUserDto => new AchievementUser
            {
                AchievementId = achievementUserDto.AchievementId,
                UserId = achievementUserDto.UserId
            }).ToList();

            return achievementUsers;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to retrieve achievement data from {URI}. {ex.Message}", ex);
        }
    }

    public static async Task AddAchivementToUser(Achievement achievement, User user, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        AchievementUserDto achievementUserDto = new()
        {
            AchievementId = achievement.Id,
            UserId = user.Id
        };

        try
        {
            var response = await httpClient.PostAsJsonAsync($"{URI}/addachievementtouser", achievementUserDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to add achievement data to {URI}. {ex.Message}", ex);
        }
    }

    public static async Task RemoveAchivementFromUser(Achievement achievement, User user, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        AchievementUserDto achievementUserDto = new()
        {
            AchievementId = achievement.Id,
            UserId = user.Id
        };

        try
        {
            var response = await httpClient.PostAsJsonAsync($"{URI}/removeachievementfromuser", achievementUserDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to remove achievement data from {URI}. {ex.Message}", ex);
        }
    }

    public static async Task AddAchievement(Achievement achievement, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        AchievementDto achievementDto = new()
        {
            Name = achievement.Name,
            Description = achievement.Description,
            Value = achievement.Value
        };

        try
        {
            var response = await httpClient.PostAsJsonAsync(URI, achievementDto);
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

        AchievementDto achievementDto = new()
        {
            Id = achievement.Id,
            Name = achievement.Name,
            Description = achievement.Description,
            Value = achievement.Value
        };

        try
        {
            var response = await httpClient.PutAsJsonAsync($"{URI}", achievementDto);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to update achievement data in {URI}. {ex.Message}", ex);
        }
    }
}