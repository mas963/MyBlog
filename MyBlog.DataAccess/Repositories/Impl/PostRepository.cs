using Microsoft.Extensions.Options;
using MyBlog.Core.Entities;
using MyBlog.DataAccess.Persistence;

namespace MyBlog.DataAccess.Repositories.Impl;

public class PostRepository : MongoDbRepositoryBase<Post>, IPostRepository
{
    public PostRepository(IOptions<MongoDbSettings> options) : base(options)
    {
    }
}
