using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MyBlog.Web.Exceptions;
using MyBlog.Web.Models.AuthorModels;
using MyBlog.Web.Models.PostModels;
using MyBlog.Web.Services;

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

    [HttpGet("GetPosts")]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await _postService.GetPostsAsync();

        return Ok(posts);
    }

    [HttpPost("AddAuthor")]
    public async Task<IActionResult> AddAuthor([FromBody] AddAuthorModel addAuthorModel)
    {
        try
        {
            await _authorService.AddAuthorAsync(addAuthorModel);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

        return Ok();
    }

    [HttpPost("AddPost")]
    public async Task<IActionResult> AddPost([FromBody] AddPostModel addPostModel)
    {
        try
        {
            await _postService.AddPostAsync(addPostModel);
        }
        catch (NotFoundException ex)
        {
            return BadRequest("not found");
        }

        return Ok();
    }

    [HttpDelete("DeletePost")]
    public async Task<IActionResult> DeletePost(string postId)
    {
        try
        {
            await _postService.DeletePostAsync(ObjectId.Parse(postId));
        }
        catch (NotFoundException ex)
        {
            return BadRequest("not found");
        }

        return Ok();
    }
}
