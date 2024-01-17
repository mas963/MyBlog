using MyBlog.Web.Models.AuthorModels;

namespace MyBlog.Web.Services;

public interface IAuthorService
{
    Task AddAuthorAsync(AddAuthorModel addAuthorModel);
}
