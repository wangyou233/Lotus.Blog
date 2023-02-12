using System.ComponentModel;

namespace Lotus.Blog.Domain.Shared.Commit;

public enum CommitStatus
{
    [Description("正常")]
    Normal,
    [Description("回收站")]
    Reclaim,
    [Description("未审核")]
    NoReview
}