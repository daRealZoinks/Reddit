using DataLayer;
using DataLayer.Dtos;
using DataLayer.Entities;
using DataLayer.Mappings;

namespace Core.Services;

public class PostCollectionService : IPostCollectionService
{
    private readonly UnitOfWork _unitOfWork;

    public PostCollectionService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public List<Post> GetAll()
    {
        var results = _unitOfWork.PostsRepository.GetAll();

        return results;
    }

    public Post? GetById(int id)
    {
        var result = _unitOfWork.PostsRepository.GetById(id);

        return result;
    }

    public void Add(Post entity)
    {
        _unitOfWork.PostsRepository.Add(entity);

        _unitOfWork.SaveChanges();
    }

    public void Update(Post entity)
    {
        var post = _unitOfWork.PostsRepository.GetById(entity.Id) ?? throw new Exception("Post not found");
        post.PostDate = entity.PostDate;
        post.Title = entity.Title;
        post.Content = entity.Content;
        post.AuthorId = entity.AuthorId;
        post.CommunityId = entity.CommunityId;

        _unitOfWork.PostsRepository.Update(entity);

        _unitOfWork.SaveChanges();
    }

    public void Delete(int id)
    {
        var post = _unitOfWork.PostsRepository.GetById(id) ?? throw new Exception("Post not found");

        _unitOfWork.PostsRepository.Remove(post);

        _unitOfWork.SaveChanges();
    }

    public PostDto? GetPostDtoById(int id)
    {
        var postDto = GetById(id)?.ToPostDto();

        return postDto;
    }

    public List<PostDto>? GetPostDtos()
    {
        var postDtos = GetAll().ToPostDtos();

        return postDtos;
    }

    public void AddPostDto(PostDto postDto)
    {
        Post post = new()
        {
            PostDate = postDto.PostDate,
            Title = postDto.Title,
            Content = postDto.Content,
            AuthorId = postDto.AuthorId,
            CommunityId = postDto.CommunityId
        };

        Add(post);
    }

    public void UpdatePostDto(PostDto postDto)
    {
        var post = GetById(postDto.Id) ?? throw new Exception("Post not found");

        post.PostDate = postDto.PostDate;
        post.Title = postDto.Title;
        post.Content = postDto.Content;
        post.AuthorId = postDto.AuthorId;
        post.CommunityId = postDto.CommunityId;

        Update(post);
    }

    public void DeletePostDto(int id)
    {
        var post = GetById(id) ?? throw new Exception("Post not found");

        Delete(post.Id);
    }
}