using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Shared.TermNode;
using Lotus.Blog.TNT.Swagger;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers;

[Route("/rss")]
[ApiExplorerSettings(GroupName = SwaggerExtensions.Grouping.GroupName_v3)]
public class RssController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly ITermNodeService _termNodeService;

    public RssController(IPostService postService,ITermNodeService termNodeService)
    {
        _postService = postService;
        _termNodeService = termNodeService;
    }
    
    
    [HttpGet]
    [ResponseCache(Duration = 1200)]
    public IActionResult Rss()
    {

        var siteUrl = _termNodeService.FindOne(x => x.Type == TermNodeType.SiteUrl);
        var feed = new SyndicationFeed("Title", "Description", new Uri(siteUrl.Code), "RSSUrl", DateTime.Now);

        feed.Copyright = new TextSyndicationContent($"{DateTime.Now.Year} Mitchel Sellers");
        var items = new List<SyndicationItem>();
        // var postings = _postService.FindAll().ToList();
        // foreach (var item in postings)
        // {
        //     // var postUrl = Url.Action("Article", "Blog", new { id = item.UrlSlug }, HttpContext.Request.Scheme);
        //     // var title = item.Title;
        //     // var description = item.Preview;                
        //     // items.Add(new SyndicationItem(title, description, new Uri(postUrl), item.UrlSlug, item.PostDate));
        // }

        feed.Items = items;
        var settings = new XmlWriterSettings
        {
            Encoding = Encoding.UTF8,
            NewLineHandling = NewLineHandling.Entitize,
            NewLineOnAttributes = true,
            Indent = true
        };
        using (var stream = new MemoryStream())
        {
            using (var xmlWriter = XmlWriter.Create(stream, settings))
            {
                var rssFormatter = new Rss20FeedFormatter(feed, false);
                rssFormatter.WriteTo(xmlWriter);
                xmlWriter.Flush();
            }
            return File(stream.ToArray(), "application/rss+xml; charset=utf-8");
        }
    }
}