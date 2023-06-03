using System.ComponentModel.DataAnnotations;

namespace RedditPublicAPI.Dtos;

public class CommentPayloadDto {
	[Required]
	public int Id {
		get; set;
	}

	[Required]
	public DateTime PostDate {
		get; set;
	}

	[Required]
	public string Content {
		get; set;
	}

	[Required]
	public int PostId {
		get; set;
	}

	[Required]
	public int AuthorId {
		get; set;
	}
}
