﻿namespace RedditPublicAPI.Dtos;

public class CommentDto {
	public int Id {
		get; set;
	}

	public DateTime PostDate {
		get; set;
	}

	public string Content {
		get; set;
	}

	public int PostId {
		get; set;
	}

	public int AuthorId {
		get; set;
	}
}
