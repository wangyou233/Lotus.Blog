using Lotus.Blog.TNT.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Web
{
    [Serializable]
    public class PageList <T>
    {
        /// <summary>
        /// 当前页
        /// </summary>
        private int _currentPage;
        /// <summary>
        /// 
        /// </summary>
        private int _pageSize;
        private int _total;
        private IList<T> _rows;
        public PageList()
        {
        }

        public PageList(IList<T> list, int total, int currentPage, int pageSize = 10)
        {
            _rows = list;

            _currentPage = currentPage;
            _total = total;
            _pageSize = pageSize == 0 ? 10 : pageSize;

        }

        public int TotalPage
        {
            get
            {
                return Total % PageSize == 0 ? Total / PageSize : Total / PageSize + 1;
            }
        }

        public int PageSize { get { return _pageSize; } set { _pageSize = value; } }

        public int Total { get { return _total; } set { _total = value; } }

        public IList<T> Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        public int CurrentPage { get { return _currentPage; } set { _currentPage = value; } }

        public object ToEasyUI()
        {
            return new { total = this.Total, rows = this.Rows };
        }

        public PageList<TView> ConvertView<TView>() where TView : new()
        {
            var list = new List<TView>();
            foreach (var o in this.Rows)
            {
                var v = new TView();
                o.CopyTo(v);

                list.Add(v);
            }

            return new PageList<TView>(list, this.Total, CurrentPage, PageSize);
        }
        /// <summary>
        /// 查询结果集合里数据项的类型转换
        /// </summary>
        /// <typeparam name="TView">转换目标类型</typeparam>
        /// <param name="Fun">转换处理</param>
        /// <returns></returns>
        public PageList<TView> ConvertView<TView>(Func<T, TView> func)
        {
            var viewList = new List<TView>();
            foreach (var r in this.Rows)
                viewList.Add(func(r));
            return new PageList<TView>(viewList, this.Total, CurrentPage, PageSize);
        }

    }
    public static class QueryExt
    {

        public static PageList<T> QueryPage<T>(this IQueryable<T> query, int page = 1, int size = 10)
        {
            int total = query.Count();

            var list = query.Skip((page - 1) * size).Take(size).ToList();

            return new PageList<T>(list, total, page, size);
        }

        public static PageList<T2> ToNewResult<T1, T2>(this PageList<T1> pagedObject, IList<T2> rows)
        {
            return new PageList<T2>(rows, pagedObject.Total, pagedObject.CurrentPage, pagedObject.PageSize);
        }
    }
    /// <summary>
    /// 分页模型
    /// </summary>
    public class PageObjectModel
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// 页数
        /// </summary>
        public int Size { get; set; } = 10;
    }
}
