using AutoMapper;
using MyBlog.Application.Models.Author;
using MyBlog.Application.Models.Post;
using MyBlog.Core.Entities;

namespace MyBlog.Application.MappingProfiles;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AddAuthorModel, Author>();

        CreateMap<AddPostModel, Post>();
    }
}
