using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Web.Controllers;

public class PostController : Controller
{
	[HttpGet]
	public async Task<IActionResult> Index()
	{
		return View();
	}
}
