using System.ComponentModel;

namespace Lotus.Blog.Domain.Shared.SystemLog;

public enum SystemLogType
{
    [Description("用户登录")]
    UserLogin,
    [Description("文章修改")]
    PostUpdate,
    [Description("文章创建")]
    PostCreate,
    [Description("文章发布")]
    PostPublish,
    [Description("登录失败")]
    LoginFailed,
    [Description("注销登录")]
    CancelLogin,
    
    [Description("博客初始化")]
    BlogInit
    
}