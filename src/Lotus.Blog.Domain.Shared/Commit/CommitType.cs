using System.ComponentModel;

namespace Lotus.Blog.Domain.Shared.Commit;

public enum CommitType
{
    [Description("文章")]
    Post,
    [Description("页面")]
    View,
    [Description("日志")]
    Log
}