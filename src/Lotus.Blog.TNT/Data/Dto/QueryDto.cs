namespace Lotus.Blog.TNT.Data.Dto;

public class QueryDto
{
    /// <summary>
    /// 当前页
    /// </summary>
    /// <value></value>
    public int Page { get; set; } = 1;

    /// <summary>
    /// 每页数量
    /// </summary>
    /// <value></value>
    public int Size { get; set; } = 20;
    
    
    /// <summary>
    /// 排序字段
    /// </summary>
    /// <value></value>
    public string OrderField { get; set; }

    /// <summary>
    /// 排序字段
    /// </summary>
    /// <value></value>
    public bool? OrderDescending { get; set; }
}