using DataLayer.Dtos;

namespace Core.Dtos;
public class CommunityPayloadDto {
	public int Id {
		get; set;
	}
	public string Name {
		get; set;
	}
	public string Description {
		get; set;
	}
	public int ModeratorId {
		get; set;
	}
	public UserDto? Moderator {
		get; set;
	}
}