using Microsoft.AspNetCore.Mvc;
using MyBlog.Web.Services;

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
