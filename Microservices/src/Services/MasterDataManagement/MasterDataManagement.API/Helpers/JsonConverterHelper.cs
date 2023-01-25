using Newtonsoft.Json;

namespace MasterDataManagement.API.Helpers
{
    public static class JsonConverterHelper
    {
        public static T ToObject<T>(this string jsonText)
        {
            return JsonConvert.DeserializeObject<T>(jsonText);
        }

        public static string ToJson<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }


    }
}