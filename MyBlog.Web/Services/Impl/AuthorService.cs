using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyBlog.Application.Helpers;
using MyBlog.Web.Models;
using MyBlog.Web.Models.AuthorModels;
using MyBlog.Web.Settings;

namespace MyBlog.Web.Services.Impl;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Author> _authorCollection;
    private readonly MongoDbSettings _settings;

    public AuthorService(IMapper mapper, IOptions<MongoDbSettings> options)
    {
        _mapper = mapper;
        _settings = options.Value;
        var client = new MongoClient(_settings.ConnectionString);
        var db = client.GetDatabase(_settings.Database);
        _authorCollection = db.GetCollection<Author>(typeof(Author).Name.ToLowerInvariant());
    }

    public async Task AddAuthorAsync(AddAuthorModel addAuthorModel)
    {
        var author = _mapper.Map<Author>(addAuthorModel);

        HashAndToken.CreatePasswordHash(addAuthorModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

        author.PasswordHash = passwordHash;
        author.PasswordSalt = passwordSalt;

        await _authorCollection.InsertOneAsync(author);
    }
}
