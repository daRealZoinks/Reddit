using RedditPublicAPI.Dtos;
using RedditPublicAPI.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RedditPublicAPI;

public class Comments {
	private const string URI = "https://localhost:5001/api/Comment";

	public static async Task<List<Comment>> GetComments(string token) {
		using HttpClient httpClient = new();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		try {
			var response = await httpClient.GetAsync(URI);
			response.EnsureSuccessStatusCode();

			var commentDtos = await response.Content.ReadFromJsonAsync<List<CommentDto>>();
			if(commentDtos == null || commentDtos.Count == 0)
				throw new Exception("No messages found.");

			return commentDtos.Select(commentDto => new Comment {
				Id = commentDto.Id,
				PostDate = commentDto.PostDate,
				Content = commentDto.Content,
				PostId = commentDto.PostId,
				AuthorId = commentDto.AuthorId
			}).ToList();
		}
		catch(HttpRequestException ex) {
			throw new Exception($"Failed to retrieve message data. {ex.Message}", ex);
		}
		catch(Exception ex) {
			throw new Exception($"Failed to retrieve message data. {ex.Message}", ex);
		}
	}

	public static async Task AddComment(Comment comment, string token) {
		using HttpClient httpClient = new();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		try {
			var response = await httpClient.PostAsJsonAsync(URI, comment);
			response.EnsureSuccessStatusCode();
		}
		catch(Exception ex) {
			throw new Exception($"Failed to add message. {ex.Message}", ex);
		}
	}

	public static async void DeleteComment(Comment comment, string token) {
		using HttpClient httpClient = new();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		try {
			var response = await httpClient.DeleteAsync($"{URI}/{comment.Id}");
			response.EnsureSuccessStatusCode();
		}
		catch(HttpRequestException ex) when(ex.Message.Contains("404")) {
			throw new InvalidOperationException($"Message with id {comment.Id} not found", ex);
		}
		catch(HttpRequestException ex) when(ex.Message.Contains("401")) {
			throw new UnauthorizedAccessException($"Unauthorized operation on message with id {comment.Id}", ex);
		}
		catch(Exception ex) {
			throw new Exception($"Failed to delete message with Id: {comment.Id}. {ex.Message}", ex);
		}
	}

	public static async void UpdateComment(Comment comment, string token) {
		using HttpClient httpClient = new();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		try {
			var response = await httpClient.PutAsJsonAsync(URI, comment);
			response.EnsureSuccessStatusCode();
		}
		catch(Exception ex) {
			Console.WriteLine(ex.Message);
			throw new Exception("Failed to update message.");
		}
	}
}
