using System;
using Lotus.Blog.TNT.Ext;
using Lotus.Blog.TNT.Web;
using Microsoft.AspNetCore.Mvc;
namespace Lotus.Blog.TNT.Attributes
{
    public abstract class BaseActionFilter:System.Attribute
    {
        
        /// <summary>
        /// 返回JSON
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns></returns>
        public ContentResult JsonContent(string json)
        {
            return new ContentResult { Content = json, StatusCode = 200, ContentType = "application/json; charset=utf-8" };
        }
        
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <returns></returns>
        public ContentResult Success()
        {
            ApiResult res = new ApiResult
            {
                Success = true,
                Code = "",
                Message = "请求成功！",
                DateTime = DateTime.Now,
            };

            return JsonContent(res.ToJson());
        }
        
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回的数据</param>
        /// <returns></returns>
        public ContentResult Success<T>(T data)
        {
            ApiResult<T> res = new ApiResult<T>
            {
                Success = true,
                Message = "请求成功！",
                Code = "",
                DateTime = DateTime.Now,
                Data = data
            };

            return JsonContent(res.ToJson());
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <returns></returns>
        public ContentResult Error()
        {
            ApiResult res = new ApiResult
            {
                Success = false,
                Message = "错误请求！",
                Code = "error",
                DateTime = DateTime.Now,
            };

            return JsonContent(res.ToJson());
        }
        public ContentResult Error(string msg)
        {
            ApiResult res = new ApiResult
            {
                Success = false,
                Message = msg,
                Code = "error",
                DateTime = DateTime.Now,
            };


            return JsonContent(res.ToJson());
        }
        /// <summary>
        /// 返回错误
        /// </summary>
        /// <param name="msg">错误提示</param>
        /// <param name="errorCode">错误代码</param>
        /// <returns></returns>
        public ContentResult Error(string msg, string errorCode)
        {
            ApiResult res = new ApiResult
            {
                Success = false,
                Message = msg,
                Code = errorCode,
                DateTime = DateTime.Now,
            };

            return JsonContent(res.ToJson());
        }
        
    }
}