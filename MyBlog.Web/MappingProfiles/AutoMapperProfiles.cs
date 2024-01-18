using AutoMapper;
using MongoDB.Bson;
using MyBlog.Web.Models;
using MyBlog.Web.Models.AuthorModels;
using MyBlog.Web.Models.PostModels;

namespace MyBlog.Web.MappingProfiles;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<AddAuthorModel, Author>();

        CreateMap<AddPostModel, Post>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => ObjectId.Parse(src.AuthorId)));
    }
}
