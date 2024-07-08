using Serialization.StringSerializers;
namespace Serialization.Serializers
{
    internal class OwnSerializer : IStringSerializer
    {
        public string ConvertToString(object obj)
        {
            if (obj == null)
            {
                return "Object is null";
            }
            else
            {
                var objectType = obj.GetType();
                var PropValues = new List<string>();
                foreach (var property in objectType.GetProperties())
                {
                    PropValues.Add($"\"{property.Name}\":{property.GetValue(obj)}");
                }
                return $"{{{string.Join(",", PropValues)}}}";
            }
        }
    }
}
