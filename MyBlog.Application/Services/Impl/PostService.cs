using AutoMapper;
using MongoDB.Driver;
using MyBlog.Application.Models.Post;
using MyBlog.Core.Entities;
using MyBlog.DataAccess.Repositories;

namespace MyBlog.Application.Services.Impl;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly IAuthorRepository _authorRepository;

    public PostService(IMapper mapper, IPostRepository postRepository, IAuthorRepository authorRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _authorRepository = authorRepository;
    }

    public async Task AddPostToAuthorAsync(AddPostModel addPostModel)
    {
        var post = _mapper.Map<Post>(addPostModel);

        await _authorRepository.AddPostToAuthorAsync(addPostModel.AuthorId, post);
        await _postRepository.AddAsync(post);
    }
}
