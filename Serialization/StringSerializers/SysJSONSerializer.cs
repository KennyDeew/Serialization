using Serialization.StringSerializers;
using System.Text.Json;

namespace Serialization.Serializers
{
    internal class SysJsonSerializer : IStringSerializer
    {
        public string ConvertToString(object obj)
        {
            if (obj == null)
            {
                return "Object is null";
            }
            else
            {
                return JsonSerializer.Serialize(obj);
            }
        }
    }
}
