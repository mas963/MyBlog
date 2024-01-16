using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.Models.Author;
using MyBlog.Application.Models.Post;
using MyBlog.Application.Services;

namespace MyBlog.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly IAuthorService _authorService;
    private readonly IPostService _postService;

    public ValuesController(IAuthorService authorService, IPostService postService)
    {
        _authorService = authorService;
        _postService = postService;
    }

    [HttpPost("AddAuthor")]
    public async Task<IActionResult> AddAuthor([FromBody] AddAuthorModel addAuthorModel)
    {
        try
        {
            await _authorService.AddAuthorAsync(addAuthorModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok();
    }

    [HttpPost("AddPost")]
    public async Task<IActionResult> AddPost([FromBody] AddPostModel addPostModel)
    {
        try
        {
            await _postService.AddPostToAuthorAsync(addPostModel);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return Ok();
    }
}
