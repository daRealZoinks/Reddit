using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mappings;

public static class CommentMappingExtension {
	public static List<CommentDto> ToCommentDtos(this List<Comment> comments) {
		if(comments == null)
			return null;

		var results = comments.Select(x => x.ToCommentDto()).ToList();

		return results;
	}

	public static CommentDto ToCommentDto(this Comment comment) {
		if(comment == null) {
			return null;
		}

		var result = new CommentDto {
			Id = comment.Id,
			PostDate = comment.PostDate,
			Content = comment.Content,
			PostId = comment.PostId,
			AuthorId = comment.AuthorId
		};

		return result;
	}
}