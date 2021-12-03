using Lotus.Blog.TNT.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Lotus.Blog.Api
{
    public static class AppExtensions
    {

        /// <summary>
        /// 自动迁移数据库
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void MigrateMarketingDatabase(this IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetService<IBaseDbRepository>();
            db?.MasterDb.Database.Migrate();
        }
    }
}
