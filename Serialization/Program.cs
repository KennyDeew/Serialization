using Serialization.Serializers;
using System.Diagnostics;

namespace Serialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Количество итераций
            int NumberOfSerilization = 100000;
            int NumberOfLoading = 10000;
            string Separator = ";";
            bool CSVwithHeader = true;
            string SeleFilePath = "C:\\Users\\a\\Documents\\";
            string CSVFilePath = SeleFilePath + "Test1.csv";
            string JSONFilePath = SeleFilePath + "Test2.json";
            //TestOwnSerializer(NumberOfSerilization);
            TestOtherSerializers(NumberOfSerilization);
            TestOwnCSVSerializer(NumberOfLoading, CSVwithHeader, Separator, CSVFilePath);
            TestOwnCSVDeserializer(NumberOfLoading, CSVwithHeader, Separator, CSVFilePath);
            TestNewtonSoftJsonSerializer(NumberOfLoading, JSONFilePath);
            TestNewtonSoftJsonDeSerializer(NumberOfLoading, JSONFilePath);
        }

        private static void TestOwnSerializer(int number)
        {
            var MySerializer = new OwnSerializer();
            Console.WriteLine($"String result - {MySerializer.ConvertToString(new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 })}");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < number; i++)
            {
                new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };
            }
            stopwatch.Stop();
            Console.WriteLine($"Without string serialization: {stopwatch.ElapsedMilliseconds}");
            stopwatch.Reset();
            Tester Test = new Tester();

            //Анализ скороски своего сериализатора
            stopwatch.Start();

            Test.TestConvertToString(MySerializer, number);

            stopwatch.Stop();
            Console.WriteLine($"With string serialization: {stopwatch.ElapsedMilliseconds}");
            stopwatch.Reset();

        }

        private static void TestOtherSerializers(int number)
        {
            var MySerializer = new OwnSerializer();
            var sysJSONSerializer = new SysJsonSerializer();
            var stopwatch = new Stopwatch();
            Tester Test = new Tester();

            //Анализ скороски своего сериализатора
            stopwatch.Start();

            Test.TestConvertToString(MySerializer, number);

            stopwatch.Stop();
            Console.WriteLine($"String serialization (by own Serializer): {stopwatch.ElapsedMilliseconds}");
            stopwatch.Reset();

            //Анализ скороски сериализатора System.Text.Json
            stopwatch.Start();

            Test.TestConvertToString(sysJSONSerializer, number);

            stopwatch.Stop();
            Console.WriteLine($"String serialization (by System.Text.Json): {stopwatch.ElapsedMilliseconds}");
            stopwatch.Reset();
        }
        private static void TestOwnCSVSerializer(int number, bool withHeader, string separator, string filePath)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < number; i++)
            {
                CSVSerializer<F> FtoCSV = new CSVSerializer<F>(separator, withHeader, new F[] { new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 } }, filePath);
                FtoCSV.Serialize();
                FtoCSV.Save();
            }
            stopwatch.Stop();
            Console.WriteLine($"Serialization to CSV (by own Loader): {stopwatch.ElapsedMilliseconds}");
        }

        private static void TestOwnCSVDeserializer(int number, bool withHeader, string separator, string filePath)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < number; i++)
            {
                CSVDeserializer<F> CSVLoader = new CSVDeserializer<F>(separator, withHeader, filePath);
                CSVLoader.ReadCSVFile();
                CSVLoader.GetPropertyMapper();
                F[] FromCSVtoFarray = CSVLoader.GetElementArrayFromCSV();
            }
            stopwatch.Stop();
            Console.WriteLine($"Load data from CSV (by own Loader): {stopwatch.ElapsedMilliseconds}");
            //Вывод в консоль полученных данных
            //CSVLoader<F> CSVLoader = new CSVLoader<F>(separator, withHeader, filePath);
            //F[] FromCSVtoFarray = CSVLoader.GetElementArrayFromCSV();
            //var MySerializer = new OwnSerializer();
            //foreach (F f in FromCSVtoFarray)
            //{
            //    Console.WriteLine(MySerializer.ConvertToString(f));
            //}
        }

        private static void TestNewtonSoftJsonSerializer(int number, string filePath)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < number; i++)
            {
                string JSONstring = NewtonJsonConverter<F>.SerializeToJSON(new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 });
                File.WriteAllText(filePath, JSONstring);
            }
            stopwatch.Stop();
            Console.WriteLine($"Serialization to JSON (by Newtonsoft): {stopwatch.ElapsedMilliseconds}");
        }

        private static void TestNewtonSoftJsonDeSerializer(int numberOfLoading,string filePath)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < numberOfLoading; i++)
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    string StrJson = r.ReadToEnd();
                    F Element = NewtonJsonConverter<F>.GetElementFromJSON(StrJson);
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Load data from JSON (by Newtonsoft): {stopwatch.ElapsedMilliseconds}");
        }
    }
}
