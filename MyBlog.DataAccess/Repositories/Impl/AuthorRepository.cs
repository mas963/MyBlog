using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MyBlog.Core.Entities;
using MyBlog.DataAccess.Persistence;

namespace MyBlog.DataAccess.Repositories.Impl;

public class AuthorRepository : MongoDbRepositoryBase<Author>, IAuthorRepository
{
    public AuthorRepository(IOptions<MongoDbSettings> options) : base(options)
    {
    }

    public async Task AddPostToAuthorAsync(string authorId, Post post)
    {
        var filter = Builders<Author>.Filter.Eq("_id", ObjectId.Parse(authorId));
        var update = Builders<Author>.Update.Push("PostIds", ObjectId.Parse(post.Id));

        await collection.UpdateOneAsync(filter, update);
    }
}
