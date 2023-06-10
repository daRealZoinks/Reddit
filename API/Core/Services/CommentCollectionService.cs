using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Mappings;

namespace Core.Services;

public class CommentCollectionService : ICommentCollectionService {
	private readonly UnitOfWork _unitOfWork;

	public CommentCollectionService(UnitOfWork unitOfWork) {
		_unitOfWork = unitOfWork;
	}

	public void Add(Comment entity) {
		_unitOfWork.CommentsRepository.Add(entity);

		_unitOfWork.SaveChanges();
	}

	public List<Comment> GetAll() {
		var results = _unitOfWork.CommentsRepository.GetAll();

		return results;
	}

	public Comment? GetById(int id) {
		var result = _unitOfWork.CommentsRepository.GetById(id);

		return result;
	}

	public void Update(Comment entity) {
		var comment = _unitOfWork.CommentsRepository.GetById(entity.Id) ?? throw new Exception("Comment not found");
		comment.PostDate = entity.PostDate;
		comment.Content = entity.Content;
		comment.PostId = entity.PostId;
		comment.Post = entity.Post;
		comment.AuthorId = entity.AuthorId;
		comment.Author = entity.Author;

		_unitOfWork.CommentsRepository.Update(entity);

		_unitOfWork.SaveChanges();
	}

	public void Delete(int id) {
		var comment = _unitOfWork.CommentsRepository.GetById(id) ?? throw new Exception("Comment not found");

		_unitOfWork.CommentsRepository.Remove(comment);

		_unitOfWork.SaveChanges();
	}

	public void AddCommentDto(CommentDto commentDto) {
		Comment comment = new() {
			PostDate = commentDto.PostDate,
			Content = commentDto.Content,
			PostId = commentDto.PostId,
			AuthorId = commentDto.AuthorId
		};

		Add(comment);
	}

	public CommentDto? GetCommentDtoById(int id) {
		var commentDto = GetById(id)?.ToCommentDto();

		return commentDto;
	}

	public List<CommentDto>? GetCommentDtos() {
		var commentDtos = GetAll().ToCommentDtos();

		return commentDtos;
	}

	public void UpdateCommentDto(CommentDto commentDto) {
		var comment = GetById(commentDto.Id) ?? throw new Exception("Comment not found");

		comment.PostDate = commentDto.PostDate;
		comment.Content = commentDto.Content;
		comment.PostId = commentDto.PostId;
		comment.AuthorId = commentDto.AuthorId;

		Update(comment);
	}

	public void DeleteCommentDto(int id) {
		var comment = GetById(id) ?? throw new Exception("Comment not found");

		Delete(comment.Id);
	}
}
