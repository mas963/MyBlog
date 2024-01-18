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
        #region Author
        CreateMap<AddAuthorModel, Author>();
        #endregion

        #region Post
        CreateMap<AddPostModel, Post>()
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => ObjectId.Parse(src.AuthorId)));

        CreateMap<GetPostsModel, Post>();
        #endregion
    }
}
