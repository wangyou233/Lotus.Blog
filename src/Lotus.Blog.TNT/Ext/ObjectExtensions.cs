using Lotus.Blog.TNT.Data.Ext;
using Lotus.Blog.TNT.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Lotus.Blog.TNT.Ext
{
    /// <summary>
    /// 对象帮助类
    /// </summary>
    public static class ObjectExtensions
    {
        public readonly static JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateFormatString = "yyyy-MM-dd hh:mm:ss",
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Converters = new List<JsonConverter>()
            {
                new StringEnumConverter()
            }
        };

        /// <summary>
        /// 对象转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        /// 复制序列中的数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="iEnumberable">原数据</param>
        /// <param name="startIndex">原数据开始复制的起始位置</param>
        /// <param name="length">需要复制的数据长度</param>
        /// <returns></returns>
        public static IEnumerable<T> Copy<T>(this IEnumerable<T> iEnumberable, int startIndex, int length)
        {
            var sourceArray = iEnumberable.ToArray();
            T[] newArray = new T[length];
            Array.Copy(sourceArray, startIndex, newArray, 0, length);

            return newArray;
        }

        /// <summary>
        /// json转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string jsonText)
        {
            if (string.IsNullOrEmpty(jsonText))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(jsonText);
        }

        /// <summary>
        /// json转对象为空则返回默认对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonText"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public static T ToObjectOrDefault<T>(this string jsonText, T o)
        {
            if (string.IsNullOrEmpty(jsonText))
            {
                return o;
            }

            return JsonConvert.DeserializeObject<T>(jsonText);
        }

        /// <summary>
        /// json转对象
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static object ToObject(this string jsonText)
        {
            if (string.IsNullOrEmpty(jsonText))
            {
                return null;
            }

            return JsonConvert.DeserializeObject(jsonText);
        }

        /// <summary>
        /// 判断某个对象是否有key
        /// </summary>
        /// <param name="data"></param>
        /// <param name="propertyname"></param>
        /// <returns></returns>
        public static bool HasProperty(this object data, string propertyname)
        {
            if (data is ExpandoObject)
                return ((IDictionary<string, object>) data).ContainsKey(propertyname);

            return data.GetType().GetProperty(propertyname) != null;
        }

        /// <summary>
        /// 获取对象int类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="propertyname"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static int GetIntProperty(this object data, string propertyname, int def = 0)
        {
            if (!data.HasProperty(propertyname))
                return def;

            if (data is ExpandoObject)
                return ((IDictionary<string, object>) data)[propertyname].TryParseInt(def);

            return data.GetType().GetProperty(propertyname).GetValue(data).TryParseInt(def);
        }

        /// <summary>
        /// 对象转int
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="def"></param>
        /// <returns></returns>
        public static int TryParseInt(this object obj, int def = 0)
        {
            if (obj == null)
                return def;

            if (obj is int)
                return (int) obj;

            return obj.ToString().ToInt().Value;
        }

        /// <summary> 
        /// 将一个object对象序列化，返回一个byte[]         
        /// </summary> 
        /// <param name="obj">能序列化的对象</param>         
        /// <returns></returns> 
        public static byte[] ToBytes(this object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.GetBuffer();
            }
        }

        /// <summary> 
        /// 将一个序列化后的byte[]数组还原         
        /// </summary>
        /// <param name="Bytes"></param>         
        /// <returns></returns> 
        public static T ToObject<T>(this byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                IFormatter formatter = new BinaryFormatter();
                return (T) formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <param name="src"></param>
        /// <param name="tar"></param>
        public static void CopyTo(this object src, object tar)
        {
            var type = src.GetType();
            var type2 = tar.GetType();

            var props = type.GetProperties();
            foreach (var prop in props)
            {
                var prop2 = type2.GetProperty(prop.Name);
                if (prop2 == null || !prop2.CanWrite)
                {
                    continue;
                }

                var val = prop.GetValue(src);

                prop2.SetValue(tar, val);
            }
        }

        public static void CopyTo(this object src, object tar, ICollection<string> includes)
        {
            var type = src.GetType();
            var type2 = tar.GetType();

            foreach (var name in includes)
            {
                if (name.IndexOf('.') > 0)
                {
                    //嵌套对象
                    var arr = name.Split('.');
                    object obj = src;
                    for (var i = 0; i < arr.Length - 1; i++)
                    {
                        var d = GetProp(obj, arr[i]);

                        if (!d.Success) //没有该属性
                            break;

                        obj = d.Result;
                        if (obj == null)
                            break;
                    }

                    if (obj != null)
                    {
                        var prop = obj.GetType().GetProperty(arr[arr.Length - 1]);
                        if (prop != null)
                        {
                            var val = prop.GetValue(obj);


                            object obj2 = tar;
                            for (var i = 0; i < arr.Length - 1; i++)
                            {
                                var p = arr[i];
                                var d2 = GetProp(obj2, p);

                                if (!d2.Success)
                                {
                                    break;
                                }

                                if (d2.Result == null) //属性值为空
                                {
                                    var p2 = obj2.GetType().GetProperty(p); //获取属性
                                    var p2type = p2.PropertyType;

                                    var newObj = Activator.CreateInstance(p2type);

                                    p2.SetValue(obj2, newObj); //创建一个新实例

                                    obj2 = newObj;
                                }
                                else
                                {
                                    obj2 = d2.Result;
                                }
                            }

                            if (obj2 != null)
                            {
                                var prop2 = obj2.GetType().GetProperty(arr[arr.Length - 1]);
                                if (prop2 != null)
                                {
                                    prop2.SetValue(obj2, val);
                                }
                            }
                        }
                    }
                }
                else
                {
                    var prop = type.GetProperty(name);
                    if (prop == null)
                        continue;

                    var val = prop.GetValue(src);

                    var prop2 = type2.GetProperty(name);
                    if (prop2 == null)
                        continue;

                    prop2.SetValue(tar, val);
                }
            }
        }

        public static ApiResult<object> GetProp(object obj, string propName)
        {
            var prop = obj.GetType().GetProperty(propName);
            if (prop == null)
                return ApiResult<object>.FailInstance("没有该属性", "prop-null");

            var val = prop.GetValue(obj);

            return ApiResult<object>.SuccessInstance(val);
        }
    }
}