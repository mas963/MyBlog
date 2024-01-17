using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MyBlog.Web.Exceptions;
using MyBlog.Web.Models;
using MyBlog.Web.Models.PostModels;
using MyBlog.Web.Settings;

namespace MyBlog.Web.Services.Impl;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Author> _authorCollection;
    private readonly IMongoCollection<Post> _postCollection;
    private readonly MongoDbSettings _settings;

    public PostService(IMapper mapper, IOptions<MongoDbSettings> options)
    {
        _mapper = mapper;
        _settings = options.Value;
        var client = new MongoClient(_settings.ConnectionString);
        var db = client.GetDatabase(_settings.Database);
        _authorCollection = db.GetCollection<Author>(typeof(Author).Name.ToLowerInvariant());
        _postCollection = db.GetCollection<Post>(typeof(Post).Name.ToLowerInvariant());
    }

    public async Task AddPostAsync(AddPostModel addPostModel)
    {
        var author = await _authorCollection.Find(x => x.Id == addPostModel.AuthorId).FirstOrDefaultAsync();

        if (author == null)
        {
            throw new NotFoundException();
        }

        var post = _mapper.Map<Post>(addPostModel);

        var filter = Builders<Author>.Filter.Eq("_id", ObjectId.Parse(addPostModel.AuthorId));
        var update = Builders<Author>.Update.Push(a => a.PostIds, ObjectId.Parse(post.Id));

        await _authorCollection.UpdateOneAsync(filter, update);

        await _postCollection.InsertOneAsync(post);
    }

    public async Task DeletePostAsync(string postId)
    {
        var deleteResult = await _postCollection.DeleteOneAsync(x => x.Id == postId);

        if (deleteResult == null)
        {
            throw new NotFoundException();
        }

        var filter = Builders<Author>.Filter.ElemMatch(a => a.PostIds, post => post == ObjectId.Parse(postId));
        var update = Builders<Author>.Update.Pull(a => a.PostIds, ObjectId.Parse(postId));

        await _authorCollection.UpdateOneAsync(filter, update);
    }
}
