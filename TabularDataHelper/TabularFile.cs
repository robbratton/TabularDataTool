using System;
using System.Data;
using System.IO;
using TabularDataHelper.Parsers;

namespace TabularDataHelper
{
    public class TabularFile
    {
        private readonly IFileParser _fileParser;

        public TabularFile(IFileParser fileParser)
        {
            _fileParser = fileParser;
        }

        public string Path { get; private set; }

        public DataTable DataTable => _fileParser.DataTable;

        public void Load(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException(nameof(path));
            }

            Path = path;

            var inputText = File.ReadAllText(path);

            _fileParser.Parse(inputText);
        }
    }
}