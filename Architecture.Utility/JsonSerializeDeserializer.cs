using Newtonsoft.Json;

namespace Architecture.Utility
{
    public static class JsonSerializeDeserializer
    {
        public static TEntity JSONStringToObject<TEntity>(string JsonStr)
        {
            return JsonConvert.DeserializeObject<TEntity>(JsonStr);
        }

        public static string JsonString<TEntity>(TEntity obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}