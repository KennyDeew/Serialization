using System;
using System.Collections.Generic;
using System.Linq;
namespace Serialization.Serializers
{
    internal class OwnSerializer : IStringSerilization
    {
        public string ConvertToString(object Object)
        {
            if (Object == null)
            {
                return "Object is null";
            }
            else
            {
                var objectType = Object.GetType();
                var PropValues = new List<string>();
                foreach (var property in objectType.GetProperties())
                {
                    PropValues.Add($"\"{property.Name}\":{property.GetValue(Object)}");
                }
                return $"{{{string.Join(",", PropValues)}}}";
            }
        }
    }



    public interface IStringSerilization
    {
        string ConvertToString(object obj);
    }
}
