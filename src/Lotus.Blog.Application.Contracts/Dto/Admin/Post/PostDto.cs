using Lotus.Blog.Application.Contracts.Dto.Admin.Tag;
using Lotus.Blog.Application.Contracts.Dto.Category;
using Lotus.Blog.Domain.Shared.Posts;
using Lotus.Blog.TNT.Data.Dto;

namespace Lotus.Blog.Application.Contracts.Dto.Admin.Post;

public class PostDto : BaseEntityDto
{
    
    public string Title { get; set; }

    public string Alias { get; set; }

    public string ImageUrl { get; set; }

    public string PassWord { get; set; }

    public string SeoSetting { get; set; }

    public string CustomDescription { get; set; }

    
    public bool IsTop { get; set; }

    
    public string Author { get; set; }

    public string Url { get; set; }

    
    public string Markdown { get; set; }

    
    public List<CategoryDto> Categories { get; set; }


    
    public PostStatus Status { get; set; }

    
    public List<TagDto> Tags { get; set; }

    public string Abstract { get; set; }

}