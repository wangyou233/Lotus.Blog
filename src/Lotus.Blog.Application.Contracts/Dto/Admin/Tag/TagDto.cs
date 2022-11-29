using Lotus.Blog.TNT.Data.Dto;

namespace Lotus.Blog.Application.Contracts.Dto.Admin.Tag;

public class TagDto : BaseEntityDto
{
    public string TagName { get; set; }

    public string DisplayName { get; set; }

    public int TagPostCount { get; set; }

}