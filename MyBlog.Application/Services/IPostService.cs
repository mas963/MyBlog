using MongoDB.Bson;
using MyBlog.Application.Models.Post;

namespace MyBlog.Application.Services;

public interface IPostService
{
    Task AddPostToAuthorAsync(AddPostModel addPostModel);
}
