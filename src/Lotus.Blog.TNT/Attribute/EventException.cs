using System;

namespace Lotus.Blog.TNT.Attribute
{
    public class EventException : Exception
    {
        /// <summary>
        /// 错误代码
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public EventException()
        {

        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        public EventException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="errorCode">错误代码</param>
        public EventException(string message, int errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}