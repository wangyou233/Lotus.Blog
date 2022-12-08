using AutoMapper;
using EnumsNET;
using Lotus.Blog.Domain.Shared.Posts;
using Lotus.Blog.TNT.Swagger;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;

namespace Lotus.Blog.Api.Controllers;

[ApiController]
[Route("enums")]
[ApiExplorerSettings(GroupName = SwaggerExtensions.Grouping.GroupName_v3)]
public class EnumsController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ILogger<EnumsController> _logger;

    public EnumsController(IMapper mapper, ILogger<EnumsController> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public Dictionary<string, IDictionary<string, string>> Index(string enumTypeString = "")
    {
        var enumTypes = enumTypeString.Split(",");

        var query = typeof(PostCommentStatus).Assembly
            .GetTypes()
            .Where(t => t.IsEnum && t.IsPublic);

        var output = new Dictionary<string, IDictionary<string, string>>();

        foreach (Type t in query)
        {
            if (enumTypeString == "all" || enumTypes.Contains(t.Name))
            {
                var values = new Dictionary<string, string>(
                    Enums.GetMembers(t)
                        .Select(item => new KeyValuePair<string, string?>(
                                item.Name,
                                item.AsString(EnumFormat.Description
                                )
                            )
                        )!
                );

                output.Add(t.Name, values);
            }
        }

        return output;
    }
}