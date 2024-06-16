using System.Text.Json;

namespace Serialization.Serializers
{
    internal class SysJSONSerializer : IStringSerilization
    {
        public string ConvertToString(object Object)
        {
            if (Object == null)
            {
                return "Object is null";
            }
            else
            {
                return JsonSerializer.Serialize(Object);
            }
        }
    }
}
