﻿using Serialization.Serializers;
using Serialization.StringSerializers;

namespace Serialization
{
    public class Tester
    {
        internal void TestConvertToString(IStringSerializer StringConverter, int CountOfConverting)
        {
            for (int i = 0; i < CountOfConverting; i++)
            {
                StringConverter.ConvertToString(new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 });
            }
        }
    }
}
