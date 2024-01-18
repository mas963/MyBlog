using MongoDB.Bson;
using MyBlog.Web.Models.PostModels;

namespace MyBlog.Web.Services;

public interface IPostService
{
    Task<List<GetPostsModel>> GetPostsAsync();
    Task AddPostAsync(AddPostModel addPostModel);
    Task DeletePostAsync(ObjectId postId);
}
