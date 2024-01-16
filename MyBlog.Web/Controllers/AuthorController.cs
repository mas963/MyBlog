using Microsoft.AspNetCore.Mvc;
using MyBlog.Application.Models.Author;
using MyBlog.Application.Services;

namespace MyBlog.Web.Controllers;

public class AuthorController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View();
    }
}
