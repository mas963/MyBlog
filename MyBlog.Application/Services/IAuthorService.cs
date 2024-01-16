using MyBlog.Application.Models.Author;

namespace MyBlog.Application.Services;

public interface IAuthorService
{
    Task AddAuthorAsync(AddAuthorModel addAuthorModel);
}
