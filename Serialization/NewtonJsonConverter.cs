using Newtonsoft.Json;

namespace Serialization
{
    internal static class NewtonJsonConverter<T> where T : new()
    {
        //CSVNewTonConverter();
        public static string SerializeToJSON(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        public static T GetElementFromJSON(string objValue)
        {
            return JsonConvert.DeserializeObject<T>(objValue);
        }
    }
}
