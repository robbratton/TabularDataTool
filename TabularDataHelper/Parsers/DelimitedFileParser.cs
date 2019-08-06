using System;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace TabularDataHelper.Parsers
{
    public class DelimitedFileParser : FileParserBase
    {
        public DelimitedFileParser(
            bool firstLineIsHeader = true,
            string delimiter = "\t")
        {
            if (string.IsNullOrEmpty(delimiter))
            {
                throw new ArgumentException($"{nameof(delimiter)} must not be null or empty.", nameof(delimiter));
            }

            FirstLineIsHeader = firstLineIsHeader;
            FileType = FileType.DelimitedText;
            Delimiter = delimiter;
        }

        public string Delimiter { get; }
        public bool FirstLineIsHeader { get; }

        public override void Parse(string input)
        {
            var configuration = new Configuration {Delimiter = Delimiter, HasHeaderRecord = FirstLineIsHeader};
            var stringReader = new StringReader(input);
            using (var csvReader = new CsvReader(stringReader, configuration))
            {
                DataTable.Clear();

                DataTable.Columns.Clear();

                var recordNumber = 0;
                while (csvReader.Read())
                {
                    recordNumber++;

                    if (recordNumber == 1)
                    {
                        if (FirstLineIsHeader)
                        {
                            csvReader.ReadHeader();

                            var header = csvReader.Context.HeaderRecord;
                            if (header != null)
                            {
                                // Make specific columns with headers
                                foreach (var column in header)
                                {
                                    DataTable.Columns.Add(column);
                                }

                                continue;
                            }
                        }
                        else
                        {
                            // Make generic columns with headers
                            for (var columnNumber = 0; columnNumber < csvReader.Context.Record.Length; columnNumber++)
                            {
                                DataTable.Columns.Add($"Column{columnNumber:0000}");
                            }
                        }
                    }

                    var newRow = DataTable.NewRow();
                    for (var columnNumber = 0; columnNumber < DataTable.Columns.Count; columnNumber++)
                    {
                        newRow[columnNumber] = csvReader.GetField(typeof(string), columnNumber);
                    }

                    DataTable.Rows.Add(newRow);
                }
            }
        }
    }
}