namespace DataLayer.Dtos;

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
	public int UserId {
		get; set;
	}
}
