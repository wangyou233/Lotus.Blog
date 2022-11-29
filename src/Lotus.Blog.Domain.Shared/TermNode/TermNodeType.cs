using System.ComponentModel;

namespace Lotus.Blog.Domain.Shared.TermNode;

public enum TermNodeType
{
    [Description("常规配置")]
    Conventional,
    [Description("SEO配置")]
    SEO,
    [Description("文章配置")]
    Post,
    [Description("评论配置")]
    Comment,
    [Description("附件配置")]
    Appendix,
    [Description("SMTP配置")]
    SMTP,
    [Description("其他配置")]
    Other
}