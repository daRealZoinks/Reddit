using DataLayer.Dtos;
using DataLayer.Entities;

namespace Core.Services;

public interface ICommentCollectionService : ICollectionService<Comment> {
	List<CommentDto>? GetCommentDtos();
	CommentDto? GetCommentDtoById(int id);
	void AddCommentDto(CommentDto commentDto);
	void UpdateCommentDto(CommentDto commentDto);
	void DeleteCommentDto(int id);
}
