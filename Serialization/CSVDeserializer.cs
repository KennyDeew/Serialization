namespace Serialization
{
    internal class CSVDeserializer<T> where T : new()
    {
        /// <summary>
        /// Массив, в котором номер элемента массива - это номер столбца csv файла, а сам элемент массива - это имя свойства класса T
        /// </summary>
        public List<string> PropertyMapper { get; set; }
        public string Separator { get; set; }
        public bool WithHeader { get; set; }
        public string FilePath { get; set; }
        public string[] CSVRows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Separator">Разделитель столбцов</param>
        /// <param name="WithHeader">есть ли строка с заголовками</param>
        /// <param name="FilePath">Путь к файлу .csv</param>
        public CSVDeserializer(string separator, bool withHeader, string filePath) 
        {
            Separator = separator;
            WithHeader = withHeader;
            FilePath = filePath;
        }

        public void ReadCSVFile()
        {
            CSVRows = File.ReadAllLines(FilePath);
        }

        public void GetPropertyMapper()
        {
            PropertyMapper = new List<string>();
            var objectType = typeof(T);
            if (CSVRows.Count() > 0)
            {
                if (WithHeader)
                {
                    //определяем Array Mapper Property
                    for (int i = 0; i < CSVRows[0].Split(Separator).Length; i++)
                    {
                        foreach (var Property in objectType.GetProperties())
                        {
                            if (Property.Name == CSVRows[0].Split(Separator)[i])
                            {
                                PropertyMapper.Add(Property.Name); 
                                break;
                            }
                        }
                    }
                }
                else if (CSVRows[0].Split(Separator).Length == objectType.GetProperties().Length)
                {
                    foreach (var Prop in objectType.GetProperties())
                    {
                        PropertyMapper.Add(Prop.Name);
                    }
                }
            }
        }
        public T GetElementFromString(string rowValue)
        {
            T element = new T();
            var objectType = typeof(T);
            if (PropertyMapper != null && PropertyMapper.Count == objectType.GetProperties().Length)
            {
                //Для каждого столбца строки CSV
                for (int i = 0; i < rowValue.Split(Separator).Length; i++)
                {
                    //Определяем имя свойства в классе с помощью MapperArray
                    foreach(var Property in objectType.GetProperties())
                    {
                        if (Property.Name == PropertyMapper[i])
                        {
                            if (Property.PropertyType.Name == typeof(int).Name && int.TryParse(rowValue.Split(Separator)[i], out int result))
                            {
                                Property.SetValue(element, result);
                            }
                        }
                    }
                }
            }
            return element;
        }
        public T[] GetElementArrayFromCSV()
        {
            List<T> elements = new List<T>();
            int StartRowNumber = WithHeader ? 1 : 0;
            for (int i = StartRowNumber; i < CSVRows.Length; i++)
            {
                elements.Add(GetElementFromString(CSVRows[i]));
            }
            return elements.ToArray();
        }
    }
}
