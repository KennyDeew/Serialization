using Serialization.Serializers;
using Serialization.StringSerializers;

namespace Serialization
{
    public class Tester
    {
        internal void TestConvertToString(IStringSerializer stringConverter, int countOfConverting)
        {
            for (int i = 0; i < countOfConverting; i++)
            {
                stringConverter.ConvertToString(new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 });
            }
        }
    }
}
