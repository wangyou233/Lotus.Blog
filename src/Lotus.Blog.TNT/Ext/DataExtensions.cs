using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.TNT.Ext;

public static class DataExtensions
{


    public static IQueryable<T> IdsToEntitys<T>(this List<int> ids) where T : Entity, new()
    {
        return ids.Select(x =>
        {
            return new T()
            {
                Id = x
            };
        }).AsQueryable();
    }
    
}