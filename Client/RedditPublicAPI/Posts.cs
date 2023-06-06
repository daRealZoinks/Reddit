using RedditPublicAPI.Dtos;
using RedditPublicAPI.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RedditPublicAPI;

public class Posts
{
    private const string URI = "https://localhost:5001/api/Post";

    public static async Task<List<Post>> GetPosts(string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.GetAsync(URI);
            response.EnsureSuccessStatusCode();

            var postDtos = await response.Content.ReadFromJsonAsync<List<PostDto>>();
            if (postDtos == null || postDtos.Count == 0)
                throw new Exception("No messages found.");

            return postDtos.Select(postDto => new Post
            {
                Id = postDto.Id,
                PostDate = postDto.PostDate,
                Title = postDto.Title,
                Content = postDto.Content,
                AuthorId = postDto.AuthorId,
                CommunityId = postDto.CommunityId
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

    public static async Task AddPost(Post post, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.PostAsJsonAsync(URI, post);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to add message. {ex.Message}", ex);
        }
    }

    public static async void DeletePost(Post post, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.DeleteAsync($"{URI}/{post.Id}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("404"))
        {
            throw new InvalidOperationException($"Message with id {post.Id} not found", ex);
        }
        catch (HttpRequestException ex) when (ex.Message.Contains("401"))
        {
            throw new UnauthorizedAccessException($"Unauthorized operation on message with id {post.Id}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to delete message with Id: {post.Id}. {ex.Message}", ex);
        }
    }

    public static async void UpdatePost(Post post, string token)
    {
        using HttpClient httpClient = new();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        try
        {
            var response = await httpClient.PutAsJsonAsync(URI, post);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception("Failed to update message.");
        }
    }
}