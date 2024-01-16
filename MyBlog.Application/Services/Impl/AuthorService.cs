using AutoMapper;
using MyBlog.Application.Helpers;
using MyBlog.Application.Models.Author;
using MyBlog.Core.Entities;
using MyBlog.DataAccess.Repositories;

namespace MyBlog.Application.Services.Impl;

public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IAuthorRepository _authorRepository;

    public AuthorService(IMapper mapper, IAuthorRepository authorRepository)
    {
        _mapper = mapper;
        _authorRepository = authorRepository;
    }

    public async Task AddAuthorAsync(AddAuthorModel addAuthorModel)
    {
        var author = _mapper.Map<Author>(addAuthorModel);

        HashAndToken.CreatePasswordHash(addAuthorModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

        author.PasswordHash = passwordHash;
        author.PasswordSalt = passwordSalt;

        await _authorRepository.AddAsync(author);
    }
}
