using AutoMapper;
using MyBlog.Web.Models;
using MyBlog.Web.Models.AuthorModels;
using MyBlog.Web.Models.PostModels;

namespace MyBlog.Web.MappingProfiles;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AddAuthorModel, Author>();

        CreateMap<AddPostModel, Post>();
    }
}
