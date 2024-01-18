using Amazon.Runtime.Internal.Util;
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
        var author = await _authorCollection.Find(x => x.Id == ObjectId.Parse(addPostModel.AuthorId)).FirstOrDefaultAsync();

        if (author == null)
        {
            throw new NotFoundException();
        }

        var post = _mapper.Map<Post>(addPostModel);
        await _postCollection.InsertOneAsync(post);

        var filter = Builders<Author>.Filter.Eq(x => x.Id, ObjectId.Parse(addPostModel.AuthorId));
        var update = Builders<Author>.Update.Push(a => a.PostIds, post.Id);

        await _authorCollection.UpdateOneAsync(filter, update);
    }

    public async Task DeletePostAsync(ObjectId postId)
    {
        var post = await _postCollection.Find(x => x.Id == postId).FirstOrDefaultAsync();
        var deleteResult = await _postCollection.DeleteOneAsync(x => x.Id == postId);

        if (deleteResult.DeletedCount == 0 || post == null)
        {
            throw new NotFoundException();
        }

        var filter = Builders<Author>.Filter.Eq(x => x.Id, post.AuthorId);
        Console.WriteLine($"postId: {postId}, post.Id: {post.Id}");
        var update = Builders<Author>.Update.Pull(u => u.PostIds, postId);

        var updateResult = await _authorCollection.UpdateOneAsync(filter, update);
    }

    public async Task<List<GetPostsModel>> GetPostsAsync()
    {
        var posts = await _postCollection.Find(_ => true).ToListAsync();

        var authorIds = posts.Select(post => post.AuthorId).ToList();
        var authors = await _authorCollection.Find(author => authorIds.Contains(author.Id)).ToListAsync();

        var postsModel = posts.Select(post => new GetPostsModel
        {
            Title = post.Title,
            Content = post.Content,
            AuthorUsername = authors.FirstOrDefault(author => author.Id == post.AuthorId)?.Username,
        }).ToList();

        return postsModel;
    }
}
