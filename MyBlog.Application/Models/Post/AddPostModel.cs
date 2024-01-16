using MongoDB.Bson;

namespace MyBlog.Application.Models.Post;

public record AddPostModel(string Title, string Content, string AuthorId);
