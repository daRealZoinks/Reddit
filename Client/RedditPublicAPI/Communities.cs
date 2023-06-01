using RedditPublicAPI.Dtos;
using RedditPublicAPI.Entities;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RedditPublicAPI;

public class Communities {
	private const string URI = "https://localhost:5001/api/Community";

	public static async Task<List<Community>> GetCommunities(string token) {
		using HttpClient httpClient = new();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		try {
			var response = await httpClient.GetAsync(URI);
			response.EnsureSuccessStatusCode();

			var communityDtos = await response.Content.ReadFromJsonAsync<List<CommunityDto>>() ??
								  throw new Exception("Failed to retrieve achievement data.");

			var communities = communityDtos.Select(communityDto => new Community {
				Id = communityDto.Id,
				Name = communityDto.Name,
				Description = communityDto.Description,
				ModeratorId = communityDto.ModeratorId,
				Moderator = communityDto.Moderator,
				Users = communityDto.Users,
			}).ToList();

			return communities;
		}
		catch(Exception ex) {
			throw new Exception($"Failed to retrieve achievement data from {URI}. {ex.Message}", ex);
		}
	}

	public static async Task AddCommunity(Community community, string token) {
		using HttpClient httpClient = new();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		CommunityPayloadDto communityPayloadDto = new() {
			Name = community.Name,
			Description = community.Description,
			ModeratorId = community.ModeratorId,
			Moderator = community.Moderator,
		};

		try {
			var response = await httpClient.PostAsJsonAsync(URI, communityPayloadDto);
			response.EnsureSuccessStatusCode();
		}
		catch(Exception ex) {
			throw new Exception($"Failed to add achievement data to {URI}. {ex.Message}", ex);
		}
	}

	public static async Task DeleteCommunity(Community community, string token) {
		using HttpClient httpClient = new();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		try {
			var response = await httpClient.DeleteAsync($"{URI}/{community.Id}");
			response.EnsureSuccessStatusCode();
		}
		catch(Exception ex) {
			throw new Exception($"Failed to delete achievement data from {URI}. {ex.Message}", ex);
		}
	}

	public static async Task UpdateCommunity(Community community, string token) {
		using HttpClient httpClient = new();
		httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

		CommunityPayloadDto communityPayloadDto = new() {
			Id = community.Id,
			Name = community.Name,
			Description = community.Description,
			ModeratorId = community.ModeratorId,
			Moderator = community.Moderator,
			Users = community.Users,
		};

		try {
			var response = await httpClient.PutAsJsonAsync($"{URI}", communityPayloadDto);
			response.EnsureSuccessStatusCode();
		}
		catch(Exception ex) {
			throw new Exception($"Failed to update achievement data in {URI}. {ex.Message}", ex);
		}
	}
}
