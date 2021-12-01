using Lotus.Blog.TNT.Data.Ext;
using Lotus.Blog.TNT.Ext;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Lotus.Blog.TNT.Data
{
    public class JsonField
    {
        [JsonExtensionData]
        private IDictionary<string, JToken> _dic = new Dictionary<string, JToken>();
        public object Get(string key, object defaultValue = null)
        {
            return _dic.ContainsKey(key) ? _dic[key] : defaultValue;
        }

        public T Get<T>(string key, T defaultValue = default(T))
        {
            return _dic.ContainsKey(key) ? _dic[key].Value<T>() : defaultValue;
        }

        public void Set(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new Exception("key is not allow to be empty");
            }

            _dic[key] = new JValue(value);
        }
    }
    public class DicField : Dictionary<string, string>, IComparer<DicField>
    {
        public int Compare([AllowNull] DicField x, [AllowNull] DicField y)
        {
            return 0;
        }
    }

    public class DicFieldJsonConverter : System.Text.Json.Serialization.JsonConverter<DicField>
    {
        public override DicField Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString().ToObject<DicField>();
        }

        public override void Write(Utf8JsonWriter writer, DicField value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToJson());
        }
    }
    public static class DicFieldExtensions
    {
        public static string GetValue(this DicField field, string name)
        {
            return (field == null || !field.ContainsKey(name)) ? string.Empty : field[name];
        }

        public static void SetValue(this DicField field, string name, string value)
        {
            if (name.IsNullOrEmpty()) return;
            if (field == null)
            {
                throw new NullReferenceException();
            }
            field[name] = value;
        }
        public static void SetInt(this DicField field, string name, int value)
        {
            field.SetValue(name, value.ToString());
        }

        public static int? GetInt(this DicField field, string name)
        {
            if (field == null || !field.ContainsKey(name))
            {
                return null;
            }

            return field[name].ToInt();
        }

        public static void SetDecimal(this DicField field, string name, decimal value)
        {
            field.SetValue(name, value.ToString());
        }

        public static decimal? GetDecimal(this DicField field, string name)
        {
            if (field == null || !field.ContainsKey(name))
            {
                return null;
            }

            return field[name].ToDecimal();
        }

        public static void SetBool(this DicField field, string name, bool value)
        {
            field.SetValue(name, value.ToString());
        }

        public static bool? GetBool(this DicField field, string name)
        {
            if (field == null || !field.ContainsKey(name))
            {
                return null;
            }

            return field[name].ToBool();
        }
    }
}
