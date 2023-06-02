using DataLayer.Dtos;
using DataLayer.Entities;

namespace Core.Services;

public interface IPostCollectionService : ICollectionService<Post> {
	List<PostDto>? GetPostDtos();
	PostDto? GetPostDtoById(int id);
	void AddPostDto(PostDto postDto);
	void UpdatePostDto(PostDto postDto);
	void DeletePostDto(int id);
}
