using Lotus.Blog.TNT.Data.Entity;

namespace Lotus.Blog.Domain.Entities
{
    public class Setting : BaseEntity
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}