using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers;

[Route("/")]
public class HomeController
{
    [HttpGet]
    public string Index()
    {
        return "Blog WebApp 1.0";
    }
}