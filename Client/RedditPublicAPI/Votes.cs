using RedditPublicAPI.Dtos;
using RedditPublicAPI.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RedditPublicAPI;

public class Votes {
	private const string URI = "https://localhost:5001/api/Vote";

	public static async Task<List<Vote>> GetVotes(string token) {
		using HttpClient httpClient = new();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		try {
			var response = await httpClient.GetAsync(URI);
			response.EnsureSuccessStatusCode();

			var voteDtos = await response.Content.ReadFromJsonAsync<List<VoteDto>>();
			if(voteDtos == null || voteDtos.Count == 0)
				throw new Exception("No votes found.");

			return voteDtos.Select(voteDto => new Vote {
				Id = voteDto.Id,
				Upvote = voteDto.Upvote,
				PostId = voteDto.PostId,
				UserId = voteDto.UserId
			}).ToList();
		}
		catch(HttpRequestException ex) {
			throw new Exception($"Failed to retrieve vote data. {ex.Message}", ex);
		}
		catch(Exception ex) {
			throw new Exception($"Failed to retrieve vote data. {ex.Message}", ex);
		}
	}

	public static async Task AddVote(Vote vote, string token) {
		using HttpClient httpClient = new();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		try {
			var response = await httpClient.PostAsJsonAsync(URI, vote);
			response.EnsureSuccessStatusCode();
		}
		catch(Exception ex) {
			throw new Exception($"Failed to add vote. {ex.Message}", ex);
		}
	}

	public static async void DeleteVote(Vote vote, string token) {
		using HttpClient httpClient = new();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		try {
			var response = await httpClient.DeleteAsync($"{URI}/{vote.Id}");
			response.EnsureSuccessStatusCode();
		}
		catch(HttpRequestException ex) when(ex.Message.Contains("404")) {
			throw new InvalidOperationException($"Vote with id {vote.Id} not found", ex);
		}
		catch(HttpRequestException ex) when(ex.Message.Contains("401")) {
			throw new UnauthorizedAccessException($"Unauthorized operation on vote with id {vote.Id}", ex);
		}
		catch(Exception ex) {
			throw new Exception($"Failed to delete vote with Id: {vote.Id}. {ex.Message}", ex);
		}
	}

	public static async void UpdateVote(Vote vote, string token) {
		using HttpClient httpClient = new();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		try {
			var response = await httpClient.PutAsJsonAsync(URI, vote);
			response.EnsureSuccessStatusCode();
		}
		catch(Exception ex) {
			Console.WriteLine(ex.Message);
			throw new Exception("Failed to update vote.");
		}
	}
}
