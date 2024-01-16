using MongoDB.Bson;
using MyBlog.Core.Entities;

namespace MyBlog.DataAccess.Repositories;

public interface IAuthorRepository : IRepository<Author, string>
{
    Task AddPostToAuthorAsync(string authorId, Post post);
}
