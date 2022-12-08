using Lotus.Blog.Domain.Shared.Posts;

namespace Lotus.Blog.Application.Contracts.Dto.Admin.Post;

public class PostCreateOrUpdateDto
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


    public List<int> CategoryIds { get; set; }


    public PostStatus Status { get; set; }


    public List<int> TagIds { get; set; }

    public string Abstract { get; set; }
}