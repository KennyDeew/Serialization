using Newtonsoft.Json;

namespace Serialization.Serializers
{
    internal class NewTonJSONSerializer : IStringSerilization
    {
        public string ConvertToString(object Object)
        {
            return JsonConvert.SerializeObject(Object);
        }
    }
}
