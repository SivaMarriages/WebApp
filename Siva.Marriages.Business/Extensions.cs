using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siva.Marriages.Business
{
    public static class Extensions
    {
        public static byte[] GetUnicodeBytes(this string str)
        {
            return Encoding.Unicode.GetBytes(str);
        }
        public static string GetUnicodeString(this byte[] bytes)
        {
            return Encoding.Unicode.GetString(bytes);
        }

        public static T? GetObjectFromUnicodeStream<T>(this MemoryStream stream) where T : new()
        {
            if (stream.Position != 0)
                stream.Position = 0;

            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            var json = bytes.GetUnicodeString();

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static Stream GetUnicodeStreamFromObject<T>(this T obj) where T : new()
        {
            var json = JsonConvert.SerializeObject(obj);
            return new MemoryStream(json.GetUnicodeBytes());
        }
    }
}
