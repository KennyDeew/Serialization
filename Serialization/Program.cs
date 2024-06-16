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
            int NumberOfLoading = 100000;
            string Separator = ";";
            bool CSVwithHeader = true;
            string CSVFilePath = "C:\\Users\\Машина\\Documents\\Test1.csv";
            string JSONFilePath = "C:\\Users\\Машина\\Documents\\Test2.json";
            //TestOwnSerializer(NumberOfSerilization);
            //TestOtherSerializers(NumberOfSerilization);
            //TestOwnCSVSerializer(NumberOfLoading, CSVwithHeader, Separator, CSVFilePath);
            //TestOwnCSVLoader(NumberOfLoading, CSVwithHeader, Separator, CSVFilePath);
            //TestNewTonSoftJSONSerialization(NumberOfLoading, JSONFilePath);
            //TestNewTonSoftLoader(NumberOfLoading, JSONFilePath);
        }

        private static void TestOwnSerializer(int N)
        {
            var MySerializer = new OwnSerializer();
            Console.WriteLine($"String result - {MySerializer.ConvertToString(new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 })}");
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < N; i++)
            {
                new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 };
            }
            stopwatch.Stop();
            Console.WriteLine($"Without string serialization: {stopwatch.ElapsedMilliseconds}");
            stopwatch.Reset();
            Tester Test = new Tester();

            //Анализ скороски своего сериализатора
            stopwatch.Start();

            Test.TestConvertToString(MySerializer, N);

            stopwatch.Stop();
            Console.WriteLine($"With string serialization: {stopwatch.ElapsedMilliseconds}");
            stopwatch.Reset();

        }

        private static void TestOtherSerializers(int N)
        {
            var MySerializer = new OwnSerializer();
            var NewTonSerializer = new NewTonJSONSerializer();
            var sysJSONSerializer = new SysJSONSerializer();
            var stopwatch = new Stopwatch();
            Tester Test = new Tester();

            //Анализ скороски своего сериализатора
            stopwatch.Start();

            Test.TestConvertToString(MySerializer, N);

            stopwatch.Stop();
            Console.WriteLine($"String serialization (by own Serializer): {stopwatch.ElapsedMilliseconds}");
            stopwatch.Reset();

            //Анализ скороски сериализатора System.Text.Json
            stopwatch.Start();

            Test.TestConvertToString(sysJSONSerializer, N);

            stopwatch.Stop();
            Console.WriteLine($"String serialization (by System.Text.Json): {stopwatch.ElapsedMilliseconds}");
            stopwatch.Reset();

            //Анализ скороски сериализатора System.Text.Json
            stopwatch.Start();

            Test.TestConvertToString(NewTonSerializer, N);

            stopwatch.Stop();
            Console.WriteLine($"String serialization (by Newtonsoft.Json): {stopwatch.ElapsedMilliseconds}");
            stopwatch.Reset();
        }
        private static void TestOwnCSVSerializer(int Num, bool CSVwithHeader, string Separator, string FilePath)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < Num; i++)
            {
                CSVSerializer<F> FtoCSV = new CSVSerializer<F>(Separator, CSVwithHeader, new F[] { new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 } }, FilePath);
            }
            stopwatch.Stop();
            Console.WriteLine($"Serialization to CSV (by own Loader): {stopwatch.ElapsedMilliseconds}");
        }

        private static void TestOwnCSVLoader(int num, bool cSVwithHeader, string separator, string filePath)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < num; i++)
            {
                CSVLoader<F> CSVLoader = new CSVLoader<F>(separator, cSVwithHeader, filePath);
                F[] FromCSVtoFarray = CSVLoader.GetElementArrayFromCSV();
            }
            stopwatch.Stop();
            Console.WriteLine($"Load data from CSV (by own Loader): {stopwatch.ElapsedMilliseconds}");
            //Вывод в консоль полученных данных
            //CSVLoader<F> CSVLoader = new CSVLoader<F>(separator, cSVwithHeader, filePath);
            //F[] FromCSVtoFarray = CSVLoader.GetElementArrayFromCSV();
            //var MySerializer = new OwnSerializer();
            //foreach (F f in FromCSVtoFarray)
            //{
            //    Console.WriteLine(MySerializer.ConvertToString(f));
            //}
        }

        private static void TestNewTonSoftJSONSerialization(int number, string filePath)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < number; i++)
            {
                string JSONstring = JSONconverter<F>.SerializeToJSON(new F() { i1 = 1, i2 = 2, i3 = 3, i4 = 4, i5 = 5 });
                File.WriteAllText(filePath, JSONstring);
            }
            stopwatch.Stop();
            Console.WriteLine($"Serialization to JSON (by Newtonsoft): {stopwatch.ElapsedMilliseconds}");
        }

        private static void TestNewTonSoftLoader(int numberOfLoading,string filePath)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < numberOfLoading; i++)
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    string StrJson = r.ReadToEnd();
                    F Element = JSONconverter<F>.GetElementFromJSON(StrJson);
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Load data from JSON (by Newtonsoft): {stopwatch.ElapsedMilliseconds}");
        }
    }
}
