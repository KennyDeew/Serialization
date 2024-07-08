namespace Serialization
{
    public class CSVSerializer<T> where T : class
    {
        internal string Separator {  get; set; }
        internal bool  WithHeader { get; set; }
        internal T[] ObjectArray { get; set; }
        internal string FilePath { get; set; }
        internal List<string> RowValues { get; set; }

        public CSVSerializer(string separator, bool withHeader, T[] objects, string filePath)
        {
            Separator = separator;
            WithHeader = withHeader;
            ObjectArray = objects;
            FilePath = filePath;
            RowValues = new List<string>();
        }

        public void Serialize()
        {
            var objectType = typeof(T);
            if (WithHeader)
            {
                //Create Header
                string Header = string.Join(Separator, objectType.GetProperties().Select(p => p.Name));
                RowValues.Add(Header);
            }
            foreach (var Obj in ObjectArray)
            {
                var PropValues = new List<string>();
                foreach (var Property in objectType.GetProperties())
                {
                    PropValues.Add(Property.GetValue(Obj).ToString());
                }
                string Row = string.Join(Separator, PropValues);
                RowValues.Add(Row);
            }
        }

        public void Save()
        {
            if (FilePath != null)
            {
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
}
