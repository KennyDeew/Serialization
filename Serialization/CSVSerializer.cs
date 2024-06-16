namespace Serialization
{
    internal class CSVSerializer<T> where T : class
    {
        internal CSVSerializer(string Separator, bool WithHeader, T[] Objects, string FilePath)
        {
            var RowValues = new List<string>();
            var objectType = typeof(T);
            if (WithHeader)
            {
                //Create Header
                string Header = string.Join(Separator, objectType.GetProperties().Select(p => p.Name));
                RowValues.Add(Header);
            }
            foreach (var Obj in Objects)
            {
                var PropValues = new List<string>();
                foreach (var Property in objectType.GetProperties())
                {
                    PropValues.Add(Property.GetValue(Obj).ToString());
                }
                string Row = string.Join(Separator, PropValues);
                RowValues.Add(Row);
            }
            using (var StrW = new StreamWriter(File.OpenWrite(FilePath)))
            {
                foreach (string RowStr in RowValues)
                {
                    StrW.WriteLine(RowStr);
                }
                
            }
        }
    }
}
