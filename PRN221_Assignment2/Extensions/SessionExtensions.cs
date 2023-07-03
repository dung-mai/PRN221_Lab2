using System.Text.Json;

namespace PRN221_Assignment2.Extensions
{
    public static class SessionExtensions
    {
        //public static T GetComplexData<T>(this ISession session, string key)
        //{
        //    var data = session.GetString(key);
        //    if (data == null)
        //    {
        //        return default(T);
        //    }
        //    return JsonConvert.DeserializeObject<T>(data);
        //}

        //public static void SetComplexData(this ISession session, string key, object value)
        //{
        //    session.SetString(key, JsonConvert.SerializeObject(value));
        //}

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
