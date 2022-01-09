using Lotus.Blog.TNT.Ext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Web
{
    public class ApiResult
    {
        /// <summary>
        /// 是否为成功请求
        /// </summary>
        public bool Success
        { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public string Code
        { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message
        { get; set; }
        
        public DateTime DateTime { get; set; }
        
    }

    public class ApiResult<T> : ApiResult
    {
        public T Data { get; set; }
        
        public static ApiResult<T> SuccessInstance(T data)
        {
            return new ApiResult<T>() { Success = true, Code = "", Message = "", Data = data };

        }

        public static ApiResult<T> FailInstance(string message, string code = "500")
        {
            return new ApiResult<T>() { Success = false, Code = code, Message = message, Data = default(T) };

        }

        public static ApiResult<T> ParamsError(params string[] ps)
        {
            return new ApiResult<T>() { Success = false, Code = "param-error", Message = "参数错误：" + ps.Join(","), Data = default(T) };
        }

        public static ApiResult<T> DataNotExist()
        {
            return new ApiResult<T>() { Success = false, Code = "not-exist", Message = "数据不存在", Data = default(T) };
        }
    }

}
