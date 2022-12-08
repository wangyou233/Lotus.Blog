using System.Linq.Expressions;
using AutoMapper;
using Lotus.Blog.Application.Contracts.Dto.Web;
using Lotus.Blog.Application.Contracts.Dto.Web.Post;
using Lotus.Blog.Application.Contracts.Services;
using Lotus.Blog.Domain.Entities;
using Lotus.Blog.TNT.Autofac;
using Lotus.Blog.TNT.Data.Ext;
using Lotus.Blog.TNT.Data.Repository;
using Lotus.Blog.TNT.Ext;
using Lotus.Blog.TNT.Service;
using Lotus.Blog.TNT.Web;

namespace Lotus.Blog.Application.Impl;

public class PostService : BaseService<Post, IBaseDbRepository>, IDependency, IPostService
{
    private readonly IMapper _mapper;

    public PostService(IBaseDbRepository repo, IMapper mapper) : base(repo)
    {
        _mapper = mapper;
    }

    public async Task<PageList<PostWebDto>> Query(PostQueryDto model)
    {
        Expression<Func<Post, bool>> where = x => !x.IsDeleted;
        if (!model.KeyWord.IsNullOrEmpty())
        {
            where = where.And(x => x.Title.Contains(model.KeyWord) || x.Markdown.Contains(model.KeyWord));
        }

        if (model.TagId != null)
        {
            where = where.And(x => x.Tags.Count(z => z.Id == model.TagId) > 0);
        }

        if (model.CategoryId != null)
        {
            where = where.And(x => x.Categories.Count(x => x.Id == model.CategoryId) > 0);
        }

        var pageResult = await FindPageAsync(new PageObjectModel()
        {
            Page = model.Page,
            Size = model.Size
        }, where, z => z.Created, new List<string>() {"Category", "Tags"});
        var result = pageResult.ConvertView<PostWebDto>(x => _mapper.Map<PostWebDto>(x));

        return result;
    }
}