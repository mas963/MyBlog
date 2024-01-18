using MongoDB.Bson;

namespace MyBlog.Web.Models.PostModels;

public record AddPostModel(string Title, string Content, string AuthorId, List<string> Tags);
