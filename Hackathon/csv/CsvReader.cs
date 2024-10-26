namespace Hackathon.csv
{
    public static class CsvReader
    {
        public static List<string[]> ReadCsvFile(string filePath, char delimiter, int skip)
        {
            var data = new List<string[]>();
            using var reader = new StreamReader(filePath);
            var lineNumber = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                lineNumber++;
                if (lineNumber <= skip)
                {
                    continue;
                }

                var values = line!.Split(delimiter);
                data.Add(values);
            }

            return data;
        }
    }
}