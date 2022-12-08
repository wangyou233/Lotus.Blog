using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers;

[Route("/")]
public class HomeController  : BaseController
{
    [HttpGet]
    public string Index()
    {
        return "Blog Api 1.0";
    }
}