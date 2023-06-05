using DataLayer.Dtos;
using DataLayer.Entities;

namespace DataLayer.Mappings;

public static class PostMappingExtension
{
    public static List<PostDto> ToPostDtos(this List<Post> posts)
    {
        if (posts == null)
            return null;

        var results = posts.Select(x => x.ToPostDto()).ToList();
        return results;
    }

    public static PostDto ToPostDto(this Post post)
    {
        if (post == null)
            return null;

        var result = new PostDto
        {
            Id = post.Id,
            PostDate = post.PostDate,
            Title = post.Title,
            Content = post.Content,
            AuthorId = post.AuthorId
        };

        return result;
    }
}